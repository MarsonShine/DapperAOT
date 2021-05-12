using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace Dapper.Internal
{





    public enum ListStrategy
    {
        None,
        Yield,
        SimpleList,
        Array,
        ImmutableArray,
        ImmutableList,
    }

    [Flags]
    public enum QueryFlags
    {
        None = 0,
        IsAsync = 1 << 0,
        IsQuery = 1 << 1,
        IsSingle = 1 << 2,
        IsIterator = 1 << 3,
        IsScalar = 1 << 4,
        DemandAtLeastOneRow = 1 << 5,
        DemandAtMostOneRow = 1 << 6,
        UseLegacyMaterializer = 1 << 7,
    }
}
