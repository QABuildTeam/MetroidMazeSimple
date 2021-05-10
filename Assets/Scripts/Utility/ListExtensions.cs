using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.List
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static void AddOrSet<T>(this List<T> list, int index, T value)
        {
            if (list != null)
            {
                while (index >= list.Count)
                {
                    list.Add(default);
                }
                list[index] = value;
            }
        }
    }
}
