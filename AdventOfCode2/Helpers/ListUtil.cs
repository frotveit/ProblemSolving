
using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public static class ListUtil
    {
        public static bool AreEqual<T>(this IList<T> data, IList<T> data2) where T: IConvertible, IComparable, IComparable<T>, IEquatable<T>
        {
            if (data.Count != data2.Count)
                return false;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].CompareTo(data2[i]) != 0)
                    return false;
            }

            return true;
        }
    }
}
