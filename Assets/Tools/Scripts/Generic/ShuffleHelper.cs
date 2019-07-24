using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class ShuffleHelper
    {
        // source : https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // source : https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle<T>(this IList<T> list, RandomGenerator random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // source : https://stackoverflow.com/questions/33643104/shuffling-a-stackt
        public static void Shuffle<T>(this Stack<T> stack)
        {
            var values = stack.ToArray();
            stack.Clear();
            foreach (var value in values.OrderBy(x => Random.Range(-100000, 100000)))
                stack.Push(value);
        }
    }
}
