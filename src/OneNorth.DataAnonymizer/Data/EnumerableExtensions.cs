using System;
using System.Collections.Generic;
using System.Linq;

namespace OneNorth.DataAnonymizer.Data
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Random<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");

            var buffer = source.ToList();
            for (var i = 0; i < count; i++)
            {
                var j = RandomProvider.GetThreadRandom().Next(0, buffer.Count);
                yield return buffer[j];
            }
        }

        public static T Random<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var buffer = source.ToList();
            var j = RandomProvider.GetThreadRandom().Next(0, buffer.Count);
            return buffer[j];
        }
    }
}