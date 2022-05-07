using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Helper method to clone Stack<T> in the correct order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalStack"></param>
        /// <returns></returns>
        public static Stack<T> CloneStack<T>(this Stack<T> originalStack)
        {
            return new Stack<T>(new Stack<T>(originalStack));
        }

    }

}
