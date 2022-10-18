using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.BeatABit.Helpers
{
    public static class CollectionUtility
    {
        [CanBeNull]
        public static T TryAccess<T>(this IList<T> source, int index) where T : class
        {
            return 0 <= index && index < source.Count ? source[index] : null;
        }
    }
}