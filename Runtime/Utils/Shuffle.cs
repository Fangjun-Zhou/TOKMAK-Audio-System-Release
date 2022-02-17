// ReSharper disable once CheckNamespace

using System;
using System.Collections.Generic;

namespace AudioSystem.Runtime.Utils
{
    /// <summary>
    /// The implementation of shuffle algorithm.
    /// </summary>
    public static class Shuffle
    {
        private static Random _random = new Random();

        /// <summary>
        /// Update the seed for the random number generator
        /// </summary>
        /// <param name="seed">the seed to update</param>
        public static void UpdateRandomSeed(int seed)
        {
            _random = new Random(seed);
        }
        
        /// <summary>
        /// The shuffle algorithm for array.
        /// </summary>
        /// <param name="array">the array to shuffle</param>
        /// <typeparam name="T">the generic type of the array</typeparam>
        public static void ArrayShuffle<T>(T[] array)
        {
            // the length of the array
            int n = array.Length;

            // iterate all the elements in the array
            for (int i = 0; i < n-1; i++)
            {
                // pick a random index
                int rand = _random.Next(i, n);
                
                // swap the element
                T tmp = array[rand];
                array[rand] = array[i];
                array[i] = tmp;
            }
        }

        /// <summary>
        /// The shuffle algorithm for List.
        /// </summary>
        /// <param name="list">the list to shuffle</param>
        /// <typeparam name="T">the generic type of the List</typeparam>
        public static void ListShuffle<T>(List<T> list)
        {
            // get the length of the List
            int n = list.Count;
            for (int i = 0; i < n-1; i++)
            {
                // pick a random index
                int rand = _random.Next(i, n);
                
                // swap the element
                T tmp = list[rand];
                list[rand] = list[i];
                list[i] = tmp;
            }
        }
    }
}