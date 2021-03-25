using System;
using System.Collections.Generic;

namespace Snap.Util
{
    public static class ListExtensions
    {
        // Fisher-Yates shuffle lifted and shifted from StackOverflow, beware only pseudorandom can't be used for gambling. 
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
