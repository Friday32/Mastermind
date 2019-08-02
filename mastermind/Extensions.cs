using System;
using System.Collections.Generic;

namespace mastermind
{
    public static class Extensions
    {
        /// <summary>
        /// Swaps elements at position1 and position2 in a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static IList<T> Swap<T>(this IList<T> list, int position1, int position2)
        {
            var temp = list[position1];
            list[position1] = list[position2];
            list[position2] = temp;
            return list;
        }

        /// <summary>
        /// Running a unit test exposed a problem using multiple Random instances.
        /// Randomization is based on the computer's clock and may not return a random
        /// result if you create multiple instances in a tight loop. A single instance
        /// will be used instead.
        /// </summary>
        private static readonly Random sRandom = new Random();

        /// <summary>
        /// Reorders elements randomly in the given list.
        /// </summary>
        /// <typeparam name="T">Type of values contained by the list</typeparam>
        /// <param name="list"></param>
        /// <returns>list</returns>
        public static IList<T> OrderRandom<T>(this IList<T> list)
        {
            for (int position = 0; position < list.Count; ++position)
            {
                // Swap the elements at the current position with a randomly
                // selected position after this position in the sequence.
                int randomPosition;
                lock (sRandom) 
                {
                    randomPosition = sRandom.Next(position, list.Count);
                }

                list.Swap(position, randomPosition);
            }

            return list;
        }
    }
}
