using System;
using System.Collections.Generic;

namespace TabTabCompiler.Tools
{
    internal static class Utils
    {
        public static void AddIfNotNull<T>(this List<T> list, T item)
        {
            if (item != null)
            {
                list.Add(item);
            }
        }

        public static bool IsEmpty<T>(this ICollection<T> col)
        {
            return col.Count == 0;
        }

        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return a.Equals(b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
