﻿using Dapper.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dapper.CodeAnalysis;

/// <summary>
/// Analyzes source for Dapper syntax and generates suitable interceptors where possible.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class DapperInterceptorGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Whether to emit interceptors even if the "interceptors" feature is not detected
    /// </summary>
    public bool OverrideFeatureEnabled { get; set; }

    /// <summary>
    /// Provide log feedback.
    /// </summary>
    public event Action<DiagnosticSeverity, string>? Log;

    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        var nodes = context.SyntaxProvider.CreateSyntaxProvider(PreFilter, Parse)
                    .Where(x => x is not null)
                    .Select((x, _) => x!);
        var combined = context.CompilationProvider.Combine(nodes.Collect());
        context.RegisterImplementationSourceOutput(combined, Generate);
    }

    private bool InterceptorsEnabled(SyntaxTree syntaxTree)
            // only even test this for things that look interesting
            => OverrideFeatureEnabled || syntaxTree.Options.Features.ContainsKey("interceptors");

    private bool PreFilter(SyntaxNode node, CancellationToken cancellation)
    {


        if (node is InvocationExpressionSyntax ie && ie.ChildNodes().FirstOrDefault() is MemberAccessExpressionSyntax ma)
        {
            var name = ma.Name.ToString();
            if (name is "Execute" or "ExecuteAsync")
            {
                Log?.Invoke(DiagnosticSeverity.Info, $"Discovered possible execution: {node}");
                return InterceptorsEnabled(node.SyntaxTree);
            }
            if (name.StartsWith("Query<") || name.StartsWith("QueryAsync<")
                || name.StartsWith("QueryFirst<") || name.StartsWith("QueryFirstAsync<")
                || name.StartsWith("QuerySingle<") || name.StartsWith("QuerySingleAsync<")
                )
            {
                Log?.Invoke(DiagnosticSeverity.Info, $"Discovered possible <T> query: {node}");
                return InterceptorsEnabled(node.SyntaxTree);
            }
        }

        return false;
    }

    // support the fact that [DapperAot(bool)] can enable/disable generation at any level
    // including method, type, module and assembly; first attribute found (walking up the tree): wins
    static bool IsEnabled(in GeneratorSyntaxContext ctx, IOperation op, CancellationToken cancellation)
    {
        var method = GetContainingMethodSyntax(op);
        if (method is not null)
        {
            var symbol = ctx.SemanticModel.GetDeclaredSymbol(method, cancellation);
            while (symbol is not null)
            {
                if (HasDapperAotAttribute(symbol, out bool enabled))
                {
                    return enabled;
                }
                symbol = symbol.ContainingSymbol;
            }
        }

        // enabled globally by default
        return true;

        static SyntaxNode? GetContainingMethodSyntax(IOperation op)
        {
            var syntax = op.Syntax;
            while (syntax is not null)
            {
                if (syntax.IsKind(SyntaxKind.MethodDeclaration))
                {
                    return syntax;
                }
                syntax = syntax.Parent;
            }
            return null;
        }
        static bool HasDapperAotAttribute(ISymbol? symbol, out bool enabled)
        {
            if (symbol is not null)
            {
                foreach (var attrib in symbol.GetAttributes())
                {
                    if (attrib.AttributeClass is
                        {
                            Name: "DapperAotAttribute",
                            ContainingNamespace:
                            {
                                Name: "Dapper",
                                ContainingNamespace: { IsGlobalNamespace: true }
                            }
                        }
                    && attrib.ConstructorArguments.Length == 1
                    && attrib.ConstructorArguments[0].Value is bool b)
                    {
                        enabled = b;
                        return true;
                    }
                }
            }

            enabled = default;
            return false;
        }
    }

    private SourceState? Parse(GeneratorSyntaxContext ctx, CancellationToken cancellation)
    {
        if (ctx.Node is not InvocationExpressionSyntax ie
            || ctx.SemanticModel.GetOperation(ie) is not IInvocationOperation op
            || !IsSupportedDapperMethod(op, out var flags))
        {
            return null;
        }

        Location? loc = null;

        if (!IsEnabled(ctx, op, cancellation))
        {
            Log?.Invoke(DiagnosticSeverity.Hidden, $"Generation disabled by attribute");
            return null;
        }

        if (op.Syntax.ChildNodes().FirstOrDefault() is MemberAccessExpressionSyntax ma)
        {
            loc = ma.ChildNodes().Skip(1).FirstOrDefault()?.GetLocation();
        }
        loc ??= op.Syntax.GetLocation();
        if (loc is null)
        {
            Log?.Invoke(DiagnosticSeverity.Hidden, $"No location found; cannot intercept");
            return null;
        }
        Log?.Invoke(DiagnosticSeverity.Hidden, $"Found {op.TargetMethod.Name}: {flags} at {loc}");

        ITypeSymbol? resultType = null, paramType = null;
        if (HasAny(flags, OperationFlags.TypedResult))
        {
            var typeArgs = op.TargetMethod.TypeArguments;
            if (typeArgs.Length == 1)
            {
                resultType = typeArgs[0];
            }
            if (resultType is null)
            {
                Log?.Invoke(DiagnosticSeverity.Hidden, $"Unable to resolve data type");
                return null;
            }
        }

        string? sql = null;
        bool? buffered = null;
        foreach (var arg in op.Arguments)
        {
            switch (arg.Parameter?.Name)
            {
                case "sql":
                    if (!TryGetConstantValue(arg, out string? s))
                    {
                        Log?.Invoke(DiagnosticSeverity.Hidden, $"Non-constant value for '{arg.Parameter?.Name}'");
                        return null;
                    }
                    sql = s;
                    break;
                case "buffered":
                    if (!TryGetConstantValue(arg, out bool b))
                    {
                        Log?.Invoke(DiagnosticSeverity.Hidden, $"Non-constant value for '{arg.Parameter?.Name}'");
                        return null;
                    }
                    buffered = b;
                    break;
                case "param":
                    if (arg.Value is not IDefaultValueOperation)
                    {
                        var expr = arg.Value;
                        if (expr is IConversionOperation conv && IsObject(expr.Type))
                        {
                            expr = conv.Operand;
                        }
                        paramType = expr?.Type;
                        if (paramType is null || IsObject(paramType))
                        {
                            Log?.Invoke(DiagnosticSeverity.Hidden, $"Cannot use parameter type '{paramType?.Name}'");
                            return null;
                        }
                        flags |= OperationFlags.HasParameters;
                    }
                    break;
                case "cnn":
                case "commandTimeout":
                case "transaction":
                    // nothing to do
                    break;
                case "commandType":
                    if (!TryGetConstantValue(arg, out int? ct))
                    {
                        Log?.Invoke(DiagnosticSeverity.Hidden, $"Non-constant value for '{arg.Parameter?.Name}'");
                        return null;
                    }
                    switch (ct)
                    {
                        case null:
                            break;
                        case 1:
                            flags |= OperationFlags.Text;
                            break;
                        case 4:
                            flags |= OperationFlags.StoredProcedure;
                            break;
                        case 512:
                            flags |= OperationFlags.TableDirect;
                            break;
                        default:
                            Log?.Invoke(DiagnosticSeverity.Hidden, $"Unexpected value for '{arg.Parameter?.Name}'");
                            return null;
                    }
                    break;
                default:
                    Log?.Invoke(DiagnosticSeverity.Hidden, $"Unexpected parameter '{arg.Parameter?.Name}'");
                    return null;
            }
        }
        if (string.IsNullOrWhiteSpace(sql))
        {
            Log?.Invoke(DiagnosticSeverity.Hidden, $"SQL not detected");
            return null;
        }
        if (HasAny(flags, OperationFlags.Query) && (buffered ?? true))
        {
            flags |= OperationFlags.Buffered;
        }
        Log?.Invoke(DiagnosticSeverity.Hidden, $"OK, {flags}, result-type: {resultType}, param-type: {paramType}");

        return new SourceState(loc, op.TargetMethod, flags, sql, resultType, paramType);

        static bool IsObject(ITypeSymbol? symbol)
            => symbol is { Name: "Object", ContainingNamespace: { Name: "System", ContainingNamespace.IsGlobalNamespace: true } };

        static bool TryGetConstantValue<T>(IArgumentOperation op, out T? value)
        {
            try
            {
                if (op.ConstantValue.HasValue)
                {
                    value = (T?)op.ConstantValue.Value;
                    return true;
                }
                else if (op.Value is ILiteralOperation or IDefaultValueOperation)
                {
                    var v = op.Value.ConstantValue;
                    value = v.HasValue ? (T?)v.Value : default;
                    return true;
                }
            }
            catch { }
            value = default!;
            return false;
        }

        static bool IsSupportedDapperMethod(IInvocationOperation operation, out OperationFlags flags)
        {
            flags = OperationFlags.None;
            var method = operation?.TargetMethod;
            if (method is null || !method.IsExtensionMethod)
            {
                return false;
            }
            var type = method.ContainingType;
            if (type is not { Name: "SqlMapper", ContainingNamespace: { Name: "Dapper", ContainingNamespace.IsGlobalNamespace: true } })
            {
                return false;
            }
            if (method.Name.EndsWith("Async"))
            {
                flags |= OperationFlags.Async;
            }
            if (method.IsGenericMethod)
            {
                flags |= OperationFlags.TypedResult;
            }
            switch (method.Name)
            {
                case "Query":
                case "QueryAsync":
                    flags |= OperationFlags.Query;
                    return method.Arity == 1;
                case "QueryFirst":
                case "QueryFirstAsync":
                    flags |= OperationFlags.First;
                    goto case "Query";
                case "QuerySingle":
                case "QuerySingleAsync":
                    flags |= OperationFlags.Single;
                    goto case "Query";
                case "Execute":
                case "ExecuteAsync":
                    flags |= OperationFlags.Execute;
                    return method.Arity == 0;
                default:
                    return false;
            }
        }
    }

    static bool HasAny(OperationFlags value, OperationFlags testFor)
        => (value & testFor) != 0;
    [Flags]
    enum OperationFlags
    {
        None = 0,
        Query = 1 << 0,
        Execute = 1 << 1,
        Async = 1 << 2,
        TypedResult = 1 << 3,
        HasParameters = 1 << 4,
        Buffered = 1 << 5,
        First = 1 << 6,
        Single = 1 << 7,
        Text = 1 << 8,
        StoredProcedure = 1 << 9,
        TableDirect = 1 << 10,
    }

    private void Generate(SourceProductionContext ctx, (Compilation Compilation, ImmutableArray<SourceState> Nodes) state)
    {
        static Dictionary<(string name, int arity, int args), IMethodSymbol> GetKnownMethods(ITypeSymbol type)
        {
            Dictionary<(string name, int arity, int args), IMethodSymbol> result = new();
            foreach (var member in type.GetMembers())
            {
                if (member is IMethodSymbol method
                    && method.IsStatic && method.DeclaredAccessibility == Accessibility.Public
                    && (method.Name.Contains("Query") || method.Name.Contains("Execute")))
                {
                    result.Add((method.Name, method.Arity, method.Parameters.Length), method);
                }
            }
            return result;
        }

        if (state.Nodes.IsDefaultOrEmpty)
        {
            // nothing to generate
            return;
        }

        var helpers = state.Compilation.GetTypeByMetadataName("Dapper.Internal.InterceptorHelpers");
        if (helpers is null)
        {
            // without InterceptorHelpers, we're toast
            return;
        }
        var knownMethods = GetKnownMethods(helpers);

        var dbCommandTypes = IdentifyDbCommandTypes(state.Compilation, out var needsCommandPrep);

        bool allowUnsafe = state.Compilation.Options is CSharpCompilationOptions cSharp && cSharp.AllowUnsafe;
        var sb = new CodeWriter().Append("file static class DapperGeneratedInterceptors").Indent()
            .DisableObsolete()
            .NewLine();
        int methodIndex = 0;

        var resultTypes = new Dictionary<ITypeSymbol, int>(SymbolEqualityComparer.Default);

        foreach (var grp in state.Nodes.GroupBy(x => x.Group(), CommonComparer.Instance))
        {
            // first, try to resolve the helper method that we're going to use for this
            var (flags, method, parameterType) = grp.Key;
            int arity = HasAny(flags, OperationFlags.TypedResult) ? 2 : 1, argCount = 8;
            bool useUnsafe = false;
            if (allowUnsafe && knownMethods.TryGetValue(("Unsafe" + method.Name, arity, argCount), out var helperMethod))
            {
                useUnsafe = true;
            }
            else if (!knownMethods.TryGetValue((method.Name, arity, argCount), out helperMethod))
            {
                // unable to find matching helper method; don't try to help!
                continue;
            }

            int usageCount = 0;
            foreach (var op in grp.OrderBy(row => row.Location, CommonComparer.Instance))
            {
                var loc = op.Location.GetLineSpan();
                var start = loc.StartLinePosition;
                sb.Append("[global::System.Runtime.CompilerServices.InterceptsLocationAttribute(")
                    .AppendVerbatimLiteral(loc.Path).Append(", ").Append(start.Line + 1).Append(", ").Append(start.Character + 1).Append(")]").NewLine();
                usageCount++;
            }

            if (usageCount == 0)
            {
                continue; // empty group?
            }

            sb.Append("internal static ").Append(useUnsafe ? "unsafe " : "").Append(method.ReturnType).Append(" ").Append(method.Name).Append(methodIndex++).Append("(");
            var parameters = method.Parameters;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0) sb.Append(", ");
                else if (method.IsExtensionMethod) sb.Append("this ");
                sb.Append(parameters[i].Type).Append(" ").Append(parameters[i].Name);
            }
            sb.Append(")").Indent().NewLine();
            sb.Append("// ").Append(flags.ToString()).NewLine();
            if (HasAny(flags, OperationFlags.HasParameters))
            {
                sb.Append("// takes parameter: ").Append(parameterType).NewLine();
            }
            ITypeSymbol? resultType = null;
            if (HasAny(flags, OperationFlags.TypedResult))
            {
                resultType = grp.First().ResultType!;
                sb.Append("// returns data: ").Append(resultType).NewLine();
            }

            // assertions
            var mode = flags & (OperationFlags.Text | OperationFlags.StoredProcedure | OperationFlags.TableDirect);
            switch (mode)
            {
                case 0:
                    sb.Append("global::System.Diagnostics.Debug.Assert(commandType is null);").NewLine();
                    // basic default for now
                    mode = OperationFlags.Text;
                    break;
                default:
                    sb.Append("global::System.Diagnostics.Debug.Assert(commandType == global::System.Data.CommandType.")
                        .Append(mode.ToString()).Append(");").NewLine();
                    break;
            }
            sb.Append("global::System.Diagnostics.Debug.Assert(param is ").Append(HasAny(flags, OperationFlags.HasParameters) ? "not " : "").Append("null);").NewLine();

            sb.Append(@"return global::Dapper.Internal.InterceptorHelpers.").Append(helperMethod.Name).Append("(").NewLine().Indent(withScope: false)
                .Append("global::Dapper.Internal.InterceptorHelpers.TypeCheck(cnn), sql,").NewLine();

            if (parameterType is null)
            {
                sb.Append("(object?)null,").NewLine();
            }
            else if (parameterType.IsAnonymousType)
            {
                sb.Append("global::Dapper.Internal.InterceptorHelpers.Reshape(param!, // transform anon-type").NewLine().Indent(false);
                AppendShapeAndTransform(sb, parameterType);
                sb.Outdent(false).NewLine();
            }
            else
            {
                sb.Append("param,").NewLine();
            }


            if (!resultTypes.TryGetValue(resultType!, out var resultTypeIndex))
            {
                resultTypes.Add(resultType!, resultTypeIndex = resultTypes.Count);
            }
            var prefix = useUnsafe ? "&" : "";
            sb.Append("global::Dapper.Internal.InterceptorHelpers.TypeCheck(transaction),").NewLine()
                .Append("commandTimeout, ")
                .Append(prefix).Append("CommandBuilder, ")
                .Append(prefix).Append("ColumnTokenizer").Append(resultTypeIndex).Append(", ")
                .Append(prefix).Append("RowReader").Append(resultTypeIndex).Append(");")
                .NewLine().Outdent(withScope: false).NewLine();

            sb.Append("static void CommandBuilder(global::System.Data.Common.DbCommand cmd, ").Append(parameterType, true).Append(" args)").Indent().NewLine();
            if (needsCommandPrep)
            {
                sb.Append("InitCommand(cmd);").NewLine();
            }
            sb.Append("cmd.CommandType = global::System.Data.CommandType.").Append(mode.ToString()).Append(";").NewLine();

            sb.Outdent().Outdent().NewLine().NewLine();
        }

        foreach (var pair in resultTypes)
        {
            /*
                 private static void ColumnTokenizer0(global::System.Data.Common.DbDataReader reader, global::System.Span<int> tokens, int fieldOffset)
    
    }

    private static Foo.Customer RowReader0(global::System.Data.Common.DbDataReader reader, global::System.ReadOnlySpan<int> tokens, int fieldOffset)
    {
       
    }
             * */
            sb.Append("private static void ColumnTokenizer").Append(pair.Value).Append("(global::System.Data.Common.DbDataReader reader, global::System.Span<int> tokens, int fieldOffset)").Indent().NewLine()
            .Append("// tokenize ").Append(pair.Key).NewLine()
            .Outdent().NewLine();
            sb.Append("private static ").Append(pair.Key).Append(" RowReader").Append(pair.Value).Append("(global::System.Data.Common.DbDataReader reader, global::System.ReadOnlySpan<int> tokens, int fieldOffset)").Indent().NewLine()
                .Append("// parse ").Append(pair.Key).NewLine()
                .Append("throw new global::System.NotImplementedException();").Outdent().NewLine();
        }

        if (needsCommandPrep)
        {
            // at least one command-type needs special handling; do that
            sb.Append("private static void InitCommand(global::System.Data.Common.DbCommand cmd)").Indent().NewLine();
            int cmdTypeIndex = 0;
            foreach (var type in dbCommandTypes)
            {
                var flags = GetSpecialCommandFlags(type);
                if (flags != SpecialCommandFlags.None)
                {
                    sb.Append("// apply special per-provider command initialization logic").NewLine()
                        .Append(cmdTypeIndex == 0 ? "" : "else ").Append("if (cmd is ").Append(type).Append(" cmd").Append(cmdTypeIndex).Append(")").Indent().NewLine();
                    if ((flags & SpecialCommandFlags.BindByName) != 0)
                    {
                        sb.Append("cmd").Append(cmdTypeIndex).Append(".BindByName = true;").NewLine();
                    }
                    if ((flags & SpecialCommandFlags.InitialLONGFetchSize) != 0)
                    {
                        sb.Append("cmd").Append(cmdTypeIndex).Append(".InitialLONGFetchSize = -1;").NewLine();
                    }
                    sb.Outdent();
                    cmdTypeIndex++;
                }
            }
            sb.Outdent().NewLine();
        }
        sb.RestoreObsolete().Outdent();

        // we need an accessible [InterceptsLocation] - if not; add our own in the generated code
        var attrib = state.Compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.InterceptsLocationAttribute");
        if (attrib is null || attrib.DeclaredAccessibility != Accessibility.Public)
        {
            sb.NewLine().Append(Resources.ReadString("Dapper.InterceptsLocationAttribute.cs"));
        }
        ctx.AddSource((state.Compilation.AssemblyName ?? "package") + ".generated.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private void AppendShapeAndTransform(CodeWriter sb, ITypeSymbol parameterType)
    {
        var members = parameterType.GetMembers();
        int count = CodeWriter.CountGettableInstanceMembers(members);
        switch (count)
        {
            case 0:
                sb.Append("static () => (object?)null,").NewLine()
                .Append("static args => null)").NewLine();
                break;
            default:
                bool first = true;
                sb.Append("static () => new {");
                foreach (var member in members)
                {
                    if (CodeWriter.IsGettableInstanceMember(member, out var type))
                    {
                        sb.Append(first ? " " : ", ").Append(member.Name).Append(" = default(").Append(type).Append(")");
                        first = false;
                    }
                }
                sb.Append(" }, // expected shape");
                if (count == 1)
                {
                    sb.Append(" "); // 1 member? fit it all on one-line
                }
                else
                {
                    sb.NewLine();
                }
                first = true;
                // note that single-member value-tuples don't exist, so go direct the value in that case
                sb.Append("static args => ").Append(count == 1 ? "" : "(");
                foreach (var member in members)
                {
                    if (CodeWriter.IsGettableInstanceMember(member, out var type))
                    {
                        sb.Append(first ? "" : ", ").Append("args.").Append(member.Name);
                        first = false;
                    }
                }
                if (count != 1)
                {
                    sb.Append(")");
                }
                break;
        }
        sb.Append(" ), // project to named type");
    }

    private static SpecialCommandFlags GetSpecialCommandFlags(ITypeSymbol type)
    {
        // check whether these command-types need special handling
        var flags = SpecialCommandFlags.None;
        foreach (var member in type.GetMembers())
        {
            switch (member.Name)
            {
                // just do a quick check for now, will be close enough
                case "BindByName" when IsSettableInstanceProperty(member, SpecialType.System_Boolean):
                    flags |= SpecialCommandFlags.BindByName;
                    break;
                case "InitialLONGFetchSize" when IsSettableInstanceProperty(member, SpecialType.System_Int32):
                    flags |= SpecialCommandFlags.InitialLONGFetchSize;
                    break;
            }
        }
        return flags;

        static bool IsSettableInstanceProperty(ISymbol? symbol, SpecialType type) =>
            symbol is IPropertySymbol prop && prop.DeclaredAccessibility == Accessibility.Public
            && prop.SetMethod is { DeclaredAccessibility: Accessibility.Public }
            && prop.Type.SpecialType == type
            && !prop.IsIndexer && !prop.IsStatic;
    }

    [Flags]
    private enum SpecialCommandFlags
    {
        None = 0,
        BindByName = 1 << 0,
        InitialLONGFetchSize = 1 << 1,
    }

    private ImmutableArray<ITypeSymbol> IdentifyDbCommandTypes(Compilation compilation, out bool needsPrepare)
    {
        needsPrepare = false;
        var dbCommand = compilation.GetTypeByMetadataName("System.Data.Common.DbCommand");
        if (dbCommand is null)
        {
            // if we can't find DbCommand, we're out of luck
            return ImmutableArray<ITypeSymbol>.Empty;
        }
        var pending = new Queue<INamespaceOrTypeSymbol>();
        foreach (var assemblyName in compilation.References)
        {
            if (assemblyName is null) continue;
            var ns = compilation.GetAssemblyOrModuleSymbol(assemblyName) switch
            {
                IAssemblySymbol assembly => assembly.GlobalNamespace,
                IModuleSymbol module => module.GlobalNamespace,
                _ => null
            };
            if (ns is not null)
            {
                pending.Enqueue(ns);
            }
        }
        var found = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        while (pending.Count != 0)
        {
            var current = pending.Dequeue();
            foreach (var member in current.GetMembers())
            {
                switch (member)
                {
                    case INamespaceSymbol ns:
                        pending.Enqueue(ns);
                        break;
                    case ITypeSymbol type:
                        // only interested in public non-static classes
                        if (!type.IsStatic && type.TypeKind == TypeKind.Class && type.DeclaredAccessibility == Accessibility.Public)
                        {
                            // note we're not checking for nested types; that seems incredibly unlikely for ADO.NET types
                            if (IsDerived(type, dbCommand))
                            {
                                found.Add(type);
                            }
                        }
                        break;
                }
            }
        }

        foreach (var type in found)
        {
            if (GetSpecialCommandFlags(type) != SpecialCommandFlags.None)
            {
                needsPrepare = true;
                break; // only need at least one
            }
        }
        return found.ToImmutableArray();

        static bool IsDerived(ITypeSymbol? type, ITypeSymbol baseType)
        {
            while (type is not null && type.SpecialType != SpecialType.System_Object)
            {
                type = type.BaseType;
                if (SymbolEqualityComparer.Default.Equals(type, baseType))
                {
                    return true;
                }
            }
            return false;
        }
    }

    sealed class SourceState
    {
        public Location Location { get; }
        public OperationFlags Flags { get; }
        public string? Sql { get; }
        public IMethodSymbol Method { get; }
        public ITypeSymbol? ResultType { get; }
        public ITypeSymbol? ParameterType { get; }
        public SourceState(Location location, IMethodSymbol method, OperationFlags flags, string? sql, ITypeSymbol? resultType, ITypeSymbol? parameterType)
        {
            Location = location;
            Flags = flags;
            Sql = sql;
            ResultType = resultType;
            ParameterType = parameterType;
            Method = method;
        }

        public (OperationFlags Flags, IMethodSymbol Method, ITypeSymbol? ParameterType) Group() => new(Flags, Method, ParameterType);


    }
    private sealed class CommonComparer : IEqualityComparer<(OperationFlags Flags, IMethodSymbol Method, ITypeSymbol? ParameterType)>, IComparer<Location>
    {
        public static readonly CommonComparer Instance = new();
        private CommonComparer() { }

        public int Compare(Location x, Location y)
        {
            if (x is null) { return y is null ? 0 : 1; }
            if (y is null) return -1;
            var xSpan = x.GetLineSpan();
            var ySpan = y.GetLineSpan();
            var delta = StringComparer.InvariantCulture.Compare(xSpan.Path, ySpan.Path);
            if (delta == 0)
            {
                delta = xSpan.StartLinePosition.CompareTo(ySpan.StartLinePosition);
            }
            if (delta == 0)
            {
                delta = xSpan.EndLinePosition.CompareTo(ySpan.EndLinePosition);
            }
            return delta;
        }

        public bool Equals((OperationFlags Flags, IMethodSymbol Method, ITypeSymbol? ParameterType) x, (OperationFlags Flags, IMethodSymbol Method, ITypeSymbol? ParameterType) y) => x.Flags == y.Flags
                && SymbolEqualityComparer.Default.Equals(x.Method, y.Method)
                && SymbolEqualityComparer.Default.Equals(x.ParameterType, y.ParameterType);

        public int GetHashCode((OperationFlags Flags, IMethodSymbol Method, ITypeSymbol? ParameterType) obj)
        {
            var hash = (int)obj.Flags;
            hash *= -47;
            hash += SymbolEqualityComparer.Default.GetHashCode(obj.Method);
            hash *= -47;
            if (obj.ParameterType is not null)
            {
                hash += SymbolEqualityComparer.Default.GetHashCode(obj.ParameterType);
            }
            return hash;
        }
    }
}
