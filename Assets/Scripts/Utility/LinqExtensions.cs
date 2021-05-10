using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

namespace Utility.Linq
{
    public static class LinqExtensions
    {
        public static List<T> ToList<T>(this IEnumerable<T> enumerator)
        {
            List<T> list = new List<T>();
            if (enumerator != null)
            {
                foreach (T item in enumerator)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static T[] ToArray<T>(this IEnumerable<T> enumerator)
        {
            if (enumerator != null)
            {
                List<T> list = enumerator.ToList();
                T[] array = new T[list.Count];
                list.CopyTo(array);
                return array;
            }
            return default;
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> enumerator, Func<T, bool> predicate)
        {
            if (enumerator != null)
            {
                foreach (T item in enumerator)
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerator, Func<T, bool> predicate = null)
        {
            if (enumerator != null)
            {
                if (predicate == null)
                {
                    foreach (T item in enumerator)
                    {
                        return item;
                    }
                }
                else
                {
                    foreach (T item in enumerator)
                    {
                        if (predicate(item))
                        {
                            return item;
                        }
                    }
                }
            }
            return default;
        }

        public static T LastOrDefault<T>(this IEnumerable<T> enumerator, Func<T, bool> predicate = null)
        {
            T foundItem = default;
            if (enumerator != null)
            {
                if (predicate == null)
                {
                    foreach (T item in enumerator)
                    {
                        foundItem = item;
                    }
                }
                else
                {
                    foreach (T item in enumerator)
                    {
                        if (predicate(item))
                        {
                            foundItem = item;
                        }
                    }
                }
            }
            return foundItem;
        }

        public static IEnumerable<O> Select<I, O>(this IEnumerable<I> enumerator, Func<I, O> converter)
        {
            if (enumerator != null)
            {
                foreach (I item in enumerator)
                {
                    yield return converter(item);
                }
            }
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> enumerator)
        {
            if (enumerator != null)
            {
                HashSet<T> hash = new HashSet<T>();
                foreach (T item in enumerator)
                {
                    hash.Add(item);
                }
                return hash;
            }
            return default;
        }

        public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> enumerator, Func<TSource, TKey> keySelector)
        {
            SortedList<TKey, List<TSource>> sortedList = new SortedList<TKey, List<TSource>>();
            if (enumerator != null)
            {
                foreach (TSource item in enumerator)
                {
                    TKey key = keySelector(item);
                    if (!sortedList.ContainsKey(key))
                    {
                        sortedList.Add(key, new List<TSource>());
                        //Debug.Log($"[{nameof(OrderBy)}] Added key {key} list");
                    }
                    sortedList[key].Add(item);
                    //Debug.Log($"[{nameof(OrderBy)}] Added item {item} to key {key} list");
                }
            }
            //int i = 0;
            foreach (KeyValuePair<TKey, List<TSource>> kvp in sortedList)
            {
                foreach (TSource item in kvp.Value)
                {
                    //++i;
                    //Debug.Log($"[{nameof(OrderBy)}] Get item {i} = {item}");
                    yield return item;
                }
            }
        }

        public static bool Any<T>(this IEnumerable<T> enumerator, Func<T, bool> predicate = null)
        {
            if (enumerator != null)
            {
                if (predicate == null)
                {
                    foreach (T item in enumerator)
                    {
                        return true;
                    }
                }
                else
                {
                    foreach (T item in enumerator)
                    {
                        if (predicate(item))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool All<T>(this IEnumerable<T> enumerator, Func<T, bool> predicate)
        {
            if (enumerator != null)
            {
                foreach (T item in enumerator)
                {
                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static int Count<T>(this IEnumerable<T> enumerator, Func<T, bool> predicate = null)
        {
            int count = 0;
            if (enumerator != null)
            {
                if (predicate == null)
                {
                    foreach (T item in enumerator)
                    {
                        ++count;
                    }
                }
                else
                {
                    foreach (T item in enumerator)
                    {
                        if (predicate(item))
                        {
                            ++count;
                        }
                    }
                }
            }
            return count;
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> enumerator, int count)
        {
            if (enumerator != null)
            {
                int i = 0;
                foreach (T item in enumerator)
                {
                    if (i > count)
                    {
                        yield return item;
                    }
                    ++i;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerator, Action<T> action)
        {
            foreach (T item in enumerator)
            {
                action?.Invoke(item);
            }
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first != null && second != null)
            {
                IEnumerator<TFirst> firstEnumerator = first.GetEnumerator();
                IEnumerator<TSecond> secondEnumerator = second.GetEnumerator();
                //Debug.Log($"[{nameof(Zip)}] Creating enumerators {firstEnumerator} and {secondEnumerator}");
                //firstEnumerator.Reset();
                //secondEnumerator.Reset();
                while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
                {
                    yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
                }
            }
        }

        public static IEnumerable<T> Join<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (T item in first)
            {
                yield return item;
            }
            foreach (T item in second)
            {
                yield return item;
            }
        }
    }
}
