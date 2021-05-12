using System.Collections.Immutable;

namespace Dapper.Internal
{
    public sealed class ParseResult
    {
        public GenerationContext Context { get; }
        public ImmutableArray<TypeWithQueries> TypesWithQueries { get; }
        public ImmutableArray<TypeForReader> TypesForReader { get; }


        internal ParseResult(GenerationContext context, ImmutableArray<TypeWithQueries> typesWithQueries, ImmutableArray<TypeForReader> typesForReader)
        {
            Context = context;
            TypesWithQueries = typesWithQueries;
            TypesForReader = typesForReader;
        }
    }
}
