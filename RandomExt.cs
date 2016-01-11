using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBGenerator
{
    public static class RandomExt
    {
        private static Random rnd = new Random();
        public static T RandomChoice<T>(this IEnumerable<T> source)
        {
            var arr = source.ToArray();
            return arr[rnd.Next(arr.Length)];
        }
        public static T RandomChoice<T>(this ICollection<T> source)
        {
            return source.ElementAt(rnd.Next(0, source.Count - 1));
        }
    }
}
