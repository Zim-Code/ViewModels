using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimCode.ViewModels
{
    /// <summary>
    /// Extension methods for <see cref="System.Collections.IEnumerable" />'s to help in the ViewModels.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Just like Linq's implementation, just with an Enumerable.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<object> Select(this IEnumerable collection, Func<object, object> selector = null)
        {
            List<object> result = new List<object>();
            foreach (var item in collection)
            {
                if (selector != null)
                    result.Add(selector.Invoke(item));
                else
                    result.Add(item);
            }
            return result.AsEnumerable();
        }

        /// <summary>
        /// Just like Linq's implementation, just with an Enumerable.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static int Count(this IEnumerable collection)
        {
            int count = 0;
            foreach (var item in collection)
                ++count;
            return count;
        }

        /// <summary>
        /// Just like Linq's implementation, just with an Enumerable.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<object> ToList(this IEnumerable collection) => new List<object>(collection.Select());
    }
}
