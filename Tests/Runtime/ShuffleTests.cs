using System;
using System.Collections;
using System.Collections.Generic;
using AudioSystem.Runtime.Utils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace AudioSystem.Tests.Runtime
{
    public class ShuffleTests
    {
        /// <summary>
        /// The test to check the input and output of the array shuffle when input is empty
        /// </summary>
        [Test]
        public void ShuffleTestArrayEmpty()
        {
            // initialize
            float[] testFloats = new float[] {};
            Debug.Log(GetElements<float>(testFloats.GetEnumerator()));
            
            // run the shuffle algorithm
            Debug.Log("Run shuffle...");
            Shuffle.ArrayShuffle(testFloats);
            
            // output
            Debug.Log(GetElements<float>(testFloats.GetEnumerator()));
        }
        
        /// <summary>
        /// The test to check the input and output of the array shuffle when input is not empty
        /// </summary>
        [Test]
        public void ShuffleTestArrayNonEmpty()
        {
            float[] testFloats = new float[] {1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f};
            Debug.Log(GetElements<float>(testFloats.GetEnumerator()));

            for (int i = 0; i < 50; i++)
            {
                // run the shuffle algorithm
                Debug.Log("Run shuffle...");
                Shuffle.ArrayShuffle(testFloats);
            
                // output
                Debug.Log(GetElements<float>(testFloats.GetEnumerator()));
            }
        }

        /// <summary>
        /// The test to check the input and output of the List shuffle when input is empty
        /// </summary>
        [Test]
        public void ShuffleTestListEmpty()
        {
            // initialize
            List<float> testFloats = new List<float>();
            Debug.Log(GetElements<float>(testFloats.GetEnumerator()));
            
            // run the shuffle algorithm
            Debug.Log("Run shuffle...");
            Shuffle.ListShuffle(testFloats);
            
            // output
            Debug.Log(GetElements<float>(testFloats.GetEnumerator()));
        }
        
        /// <summary>
        /// The test to check the input and output of the List shuffle when input is not empty
        /// </summary>
        [Test]
        public void ShuffleTestListNonEmpty()
        {
            List<float> testFloats = new List<float>() {1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f};
            Debug.Log(GetElements<float>(testFloats.GetEnumerator()));

            for (int i = 0; i < 50; i++)
            {
                // run the shuffle algorithm
                Debug.Log("Run shuffle...");
                Shuffle.ListShuffle(testFloats);
            
                // output
                Debug.Log(GetElements<float>(testFloats.GetEnumerator()));
            }
        }

        /// <summary>
        /// Get a string representation of the whole array.
        /// </summary>
        /// <param name="array">the array to debug</param>
        /// <typeparam name="T">the generic in the array</typeparam>
        /// <returns>the string representation of the array</returns>
        private string GetElements<T>(IEnumerator enumerator)
        {
            bool finished = !enumerator.MoveNext();
            
            string res = "[";
            
            while (!finished)
            {
                T item = (T)enumerator.Current;
                
                res += item.ToString();
                finished = !enumerator.MoveNext();
                if (!finished)
                {
                    res += ", ";
                }
            }

            res += "]";
            return res;
        }
    }
}
