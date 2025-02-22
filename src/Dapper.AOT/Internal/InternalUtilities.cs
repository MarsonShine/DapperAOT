﻿using System;
using System.Buffers;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Dapper.Internal;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// This type is not intended for public consumption. Please just don't, thanks.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete(InternalUtilities.ObsoleteWarning)]
public static partial class InternalUtilities
{
   

    internal const string ObsoleteWarning = "This type is not intended for public consumption, and can change without warning.";

    static readonly object[] s_BoxedInt32 = new object[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    static readonly object s_BoxedTrue = true, s_BoxedFalse = false;

    public static object AsValue(int value)
        => value >= -1 && value <= 10 ? s_BoxedInt32[value + 1] : value;
    public static object AsValue(int? value)
        => value.HasValue ? AsValue(value.GetValueOrDefault()) : DBNull.Value;
    public static object AsValue(bool value)
        => value ? s_BoxedTrue : s_BoxedFalse;
    public static object AsValue(bool? value)
        => value.HasValue ? AsValue(value.GetValueOrDefault()) : DBNull.Value;
    // ... and a few others

    public static object AsValue(object value)
        => value ?? DBNull.Value;

    public static int GetFieldNumbers(Span<int> fieldNumbers, IDataReader reader, Func<string, int> selector)
    {
        var count = reader.FieldCount;
        for (int i = 0; i < count; i++)
        {
            fieldNumbers[i] = selector(reader.GetName(i));
        }
        return count;
    }

    private static readonly int[] s_One = new int[1];

#pragma warning disable IDE0079 // "unnecessary suppression" - CA1806 is framework dependent
#pragma warning disable CA1806 // "LINQ can have side effects" - not here, it can't
    public static void ThrowMultiple() => s_One.Single();
    public static void ThrowNone() => Array.Empty<int>().Single();
#pragma warning restore CA1806
#pragma warning restore IDE0079
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member