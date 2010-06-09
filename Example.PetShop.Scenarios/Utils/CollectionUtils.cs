#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace Example.PetShop.Scenarios.Utils
{
    public class CollectionUtils
    {
        public delegate TN SingleObjectConverter<TN>(object o);

        public static List<TN> Convert<TN>(IEnumerable originals, SingleObjectConverter<TN> converter)
        {
            var list = new List<TN>();
            foreach (var original in originals)
            {
                list.Add(converter(original));
            }
            return list;
        }
    }
}