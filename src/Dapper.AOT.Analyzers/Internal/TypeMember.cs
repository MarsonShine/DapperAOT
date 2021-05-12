using Microsoft.CodeAnalysis;
using System;

namespace Dapper.Internal
{
    public sealed class TypeMember
    {
        public ISymbol Member { get; }

        internal static TypeMember? TryCreate(ISymbol member)
        {
            if (member is null || member.IsStatic) return null;

            throw new NotImplementedException();
        }

        internal TypeMember(ISymbol member)
        {
            Member = member;
        }
    }
}
