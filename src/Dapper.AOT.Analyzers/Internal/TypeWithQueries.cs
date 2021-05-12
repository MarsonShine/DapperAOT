using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Dapper.Internal
{
    public sealed class TypeWithQueries
    {
        public INamedTypeSymbol Type { get; }
        public ImmutableArray<Query> Queries { get; }
        public string Namespace { get; }
        public string TypeName { get; }
        internal TypeWithQueries(INamedTypeSymbol type, ImmutableArray<Query> queries)
        {
            Type = type;
            Queries = queries;
            Namespace = GetNamespace(type);
            TypeName = GetTypeName(type);
            static string GetNamespace(INamedTypeSymbol? type)
            {
                static bool IsRoot([NotNullWhen(false)] INamespaceSymbol? ns)
                    => ns is null || ns.IsGlobalNamespace;

                var ns = type?.ContainingNamespace;
                if (IsRoot(ns)) return "";
                if (IsRoot(ns.ContainingNamespace)) return ns.Name;
                var sb = new StringBuilder();
                WriteAncestry(ns.ContainingNamespace, sb);
                sb.Append(ns.Name);
                return sb.ToString();

                static void WriteAncestry(INamespaceSymbol? ns, StringBuilder sb)
                {
                    if (!IsRoot(ns))
                    {
                        WriteAncestry(ns.ContainingNamespace, sb);
                        sb.Append(ns.Name).Append('.');
                    }
                }
            }
            static string GetTypeName(INamedTypeSymbol? type)
            {
                if (type is null) return "";
                var parent = type.ContainingType;
                if (parent is null && !IsGeneric(type)) return type.Name ?? "";
                var sb = new StringBuilder();
                WriteAncestry(type, sb, false);
                return sb.ToString();

                static bool IsGeneric(INamedTypeSymbol type)
                    => type.IsGenericType && !type.TypeArguments.IsDefaultOrEmpty;

                static void WriteType(INamedTypeSymbol type, StringBuilder sb)
                {
                    sb.Append(type.Name);
                    if (IsGeneric(type))
                    {
                        sb.Append('<');
                        bool first = true;
                        foreach (var t in type.TypeArguments)
                        {
                            if (!first) sb.Append(", ");
                            first = false;
                            sb.Append(t.Name);
                        }
                        sb.Append('>');
                    }
                }
                static void WriteAncestry(INamedTypeSymbol? type, StringBuilder sb, bool withSuffix)
                {
                    if (type is not null)
                    {
                        WriteAncestry(type.ContainingType, sb, true);
                        WriteType(type, sb);
                        if (withSuffix) sb.Append('.');
                    }
                }
            }
        }


    }
}
