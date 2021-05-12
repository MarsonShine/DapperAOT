using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Dapper.Internal
{
    public sealed class GenerationContext
    {
        private ConcurrentQueue<Diagnostic>? _diagnostics; // lazy, inspired by DiagnosticBag
        public Compilation Compilation { get; }
        public GenerationContext(Compilation compilation)
            => Compilation = compilation;

        public void ReportDiagnostic(Diagnostic diagnostic)
            => (_diagnostics ??= new()).Enqueue(diagnostic);

        public event Action<DiagnosticSeverity, string>? Log;
        public void ReportDiagnostics(in GeneratorExecutionContext context)
        {
            var pending = _diagnostics;
            if (pending is not null)
            {
                while (pending.TryDequeue(out var d))
                    context.ReportDiagnostic(d);
            }
        }

        public ParseResult Parse(List<MethodDeclarationSyntax> methods)
        {
            List<(INamedTypeSymbol Type, IMethodSymbol Method, MethodDeclarationSyntax Syntax)>? candidates = null;
            if (methods is not null)
            {
                foreach (var syntaxNode in methods)
                {
                    try
                    {
                        if (Compilation.GetSemanticModel(syntaxNode.SyntaxTree).GetDeclaredSymbol(syntaxNode) is not IMethodSymbol method)
                            continue; // couldn't find it, or wasn't a method

                        if (method.PartialImplementationPart is not null) continue; // already has an implementation

                        if (method.IsGenericMethod) continue; // generics not implemented yet

                        if (method.ContainingType is not INamedTypeSymbol type) continue; // need a declaring type

                        var ca = method.GetAttributes().SingleOrDefault(x => x.AttributeClass.IsExact("Dapper", "CommandAttribute", 0));
                        if (ca is null) continue; // lacking [Command]

                        Log?.Invoke(DiagnosticSeverity.Info, $"Detected candidate: '{method.Name}'");
                        (candidates ??= new()).Add((type, method, syntaxNode));
                    }
                    catch (Exception ex)
                    {
                        Log?.Invoke(DiagnosticSeverity.Error, $"Error processing '{syntaxNode.Identifier}': '{ex.Message}'");
                        // TODO: declare a formal diagnostic for this
                        var err = Diagnostic.Create("DAP001", "Dapper", ex.Message, DiagnosticSeverity.Warning, DiagnosticSeverity.Warning, true, 4, location: syntaxNode.GetLocation());
                        ReportDiagnostic(err);
                    }
                }
            }
            List<TypeWithQueries>? typesWithQueries = null;
            if (candidates is not null)
            {
                List<Query>? _queries = null;
                foreach (var grp in candidates.GroupBy(x => x.Type, (IEqualityComparer<INamedTypeSymbol>)SymbolEqualityComparer.Default))
                {
                    foreach (var method in grp)
                    {
                        var query = Query.TryCreate(this, method.Syntax, method.Method);
                        if (query is not null)
                        {
                            (_queries ??= new()).Add(query);
                        }
                    }
                    if (_queries is not null && _queries.Count != 0)
                    {
                        (typesWithQueries ??= new()).Add(new TypeWithQueries(grp.Key, _queries.ToImmutableArray()));
                        _queries.Clear();
                    }
                }
            }
            if (typesWithQueries is null)
            {
                return new ParseResult(this, ImmutableArray<TypeWithQueries>.Empty, ImmutableArray<TypeForReader>.Empty);
            }

            typesWithQueries.Sort((x, y) =>
            {
                int delta = string.Compare(x.Namespace, y.Namespace, StringComparison.Ordinal);
                if (delta == 0) delta = string.Compare(x.TypeName, y.TypeName, StringComparison.Ordinal);
                return delta;
            });

            return new ParseResult(this, typesWithQueries.ToImmutableArray(), ImmutableArray<TypeForReader>.Empty);

        }
    }
}
