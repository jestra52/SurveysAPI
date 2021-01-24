using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Surveys.Common.Util
{
    public static class List
    {
        public static List<T> SwapPosition<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;

            return list.ToList();
        }
    }
}
