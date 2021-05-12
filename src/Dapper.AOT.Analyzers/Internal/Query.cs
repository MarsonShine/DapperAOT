using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Dapper.Internal
{
    public sealed class Query
    {
        public IMethodSymbol Method { get; }
        public QueryFlags Flags { get; }
        public ITypeSymbol? ItemType { get; }
        public NullableAnnotation ItemNullability { get; }
        public ListStrategy ListStrategy { get; }
        public ITypeSymbol ListType { get; }

        public static Query? TryCreate(GenerationContext context, MethodDeclarationSyntax syntax, IMethodSymbol method)
        {
            return null;
        }

        internal Query(IMethodSymbol method, QueryFlags flags,
            ITypeSymbol? itemType, NullableAnnotation itemNullability,
            ListStrategy listStrategy, ITypeSymbol listType)
        {
            Method = method;
            Flags = flags;
            ItemType = itemType;
            ItemNullability = itemNullability;
            ListStrategy = listStrategy;
            ListType = listType;
        }
    }
}
