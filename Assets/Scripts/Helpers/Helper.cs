using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public static class Helper
    {
        private static System.Random _random = new System.Random();

        /// <summary>
        /// Clone Stack<T> in the correct order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalStack"></param>
        /// <returns></returns>
        public static Stack<T> CloneStack<T>(this Stack<T> originalStack)
        {
            return new Stack<T>(new Stack<T>(originalStack));
        }

        /// <summary>
        /// Gets Random value from an Enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns name="T"></returns>
        public static T RandomEnumValue<T>()
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(_random.Next(values.Length));
        }

        /// <summary>
        /// Get Distinct Array of values from an enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size"></param>
        /// <returns name="T[]"></returns>
        public static T[] GetDistinctEnumValues<T>(int size)
        {
            if (size == 0) return null;

            var values = Enum.GetValues(typeof(T));

            if (size > values.Length) size = values.Length;

            List<T> valuesList = new List<T>();

            for (int i = 0; i < size; i++)
            {
                var distinctValue = (T)values.GetValue(_random.Next(values.Length));
                while (valuesList.Contains(distinctValue))
                {
                    distinctValue = (T)values.GetValue(_random.Next(values.Length));
                }

                valuesList.Add(distinctValue);
            }

            return valuesList.ToArray();
        }
    }

}
