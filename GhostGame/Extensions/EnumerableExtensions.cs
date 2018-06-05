using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostGame
{
    public static class EnumerableExtensions
    {
        public static T PickRandomElement<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
