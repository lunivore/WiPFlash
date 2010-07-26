#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace WiPFlash.Util
{
    public class CollectionUtils
    {
        public delegate T SingleObjectConverter<T>(object o);

        public static List<T> Convert<T>(IEnumerable originals, SingleObjectConverter<T> converter)
        {
            var list = new List<T>();
            foreach (var original in originals)
            {
                list.Add(converter(original));
            }
            return list;
        }

        public static int IndexOf<T>(IEnumerable<T> candidates, Predicate<T> matcher)
        {
            int count = 0;
            foreach (var candidate in candidates)
            {
                if (matcher(candidate))
                {
                    return count;
                }
                count++;
            }
            return -1;
        }
    }
}