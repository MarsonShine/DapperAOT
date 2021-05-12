using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Dapper.Internal
{
    public sealed class TypeForReader
    {
        public INamedTypeSymbol Type { get; }

        internal TypeForReader(INamedTypeSymbol type, ImmutableArray<TypeMember> members)
        {
            Type = type;
        }
    }
}
