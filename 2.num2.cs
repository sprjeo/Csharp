using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;

namespace Project
{

    class Program
    {
        static void Main()
        {
            try
            {
                int[] set1 = { 1, 2, 3 };
                int[] set2 = { 1, 2 };
                int[] set3 = { 1, 2, 3 };
                var comparer = new IntComparer();

                var list = new List<int>();
                Console.WriteLine(EnumerableToString(
                    set1.GetGenerationCombinationsWithElementRepetition(2, comparer)));

                Console.WriteLine(EnumerableToString(set2.GetGenerationSubset(comparer)));

                Console.WriteLine(EnumerableToString(set3.GetGenerationPermutationWithoutElementRepetition(comparer)));
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        static string EnumerableToString<T>(IEnumerable<IEnumerable<T>> collections)
        {
            string result = "";
            result += "[";

            foreach (var collection in collections)
            {
                string collectionString = "";
                collectionString += " [ ";
                collectionString += string.Join(", ", collection);
                collectionString += " ],";
                result += collectionString;
            }

            result = result[..^1];
            result += " ]";
            return result;
        }
    }

    class IntComparer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y)
        {
            return x == y;
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }



    public static class EnumerableExtensions   // проверка на уникальность
    {
        private static void ThrowIfNotDistinct<T>(
            this IEnumerable<T> values,
            IEqualityComparer<T> equalityComparer)
        {
            if (values.Distinct(equalityComparer).Count() != values.Count())
            {
                throw new ArgumentException("Elements are repeated.", nameof(values));
            }
        }

        /* генерация всех возможных сочетаний из n (кол-во элементов
        перечисления) по k (с точностью до порядка, элементы могут
        повторяться) из элементов входного перечисления:
            Входное перечисление: [1, 2, 3]; k == 2
            Выходное перечисление: [ [1, 1], [1, 2], [1, 3], [2, 2], [2, 3], [3, 3] ] */

        public static IEnumerable<IEnumerable<T>> GetGenerationCombinationsWithElementRepetition<T>(
            this IEnumerable<T>? collection, int k, IEqualityComparer<T>? comparer)
        {


            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection), " collection shouldn't be null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(collection), " comparer shouldn't be null.");

            }

            ThrowIfNotDistinct(collection, comparer);

            if (k > collection.Count())
            {
                throw new ArgumentException(" Parameter k must not be less than the length of the collection.", nameof(collection));
            }

            return collection.GenerationCombinationsWithElementRepetition(k);
        }

        public static IEnumerable<IEnumerable<T>> GenerationCombinationsWithElementRepetition<T>(
            this IEnumerable<T> collection, int k)
        {
            if (k <= 0)
            {
                yield return new List<T>();
            }
            else
            {
                int curr = 0; // исключение повторений самих сочетаний
                foreach (var i in collection)
                {
                    foreach (var j in collection.Skip(curr++).GenerationCombinationsWithElementRepetition<T>(k - 1))
                    {
                        List<T> tmpCollection = new List<T> { i };
                        tmpCollection.AddRange(j);
                        yield return tmpCollection;
                    }
                }
            }
        }

        /* генерация всех возможных подмножеств (без повторений) из
        элементов входного перечисления:
            Входное перечисление: [1, 2]
            Выходное перечисление: [ [], [1], [2], [1, 2] ] */

        public static IEnumerable<IEnumerable<T>> GetGenerationSubset<T>(
            this IEnumerable<T>? collection, IEqualityComparer<T>? comparer)
        {

            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection), "collection shouldn't be null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer), "comparer shouldn't be null.");

            }

            ThrowIfNotDistinct(collection, comparer);

            
            int degree = 1, countCollection = collection.Count();
            int tmp = countCollection;
            while (tmp > 0)
            {
                degree *= 2;
                tmp--;
            }

            var resultList = new List<IEnumerable<T>>();
            var elementList = new List<T>();
            for (int i = 0; i < degree; i++) 
            {
                for (int j = 0; j < countCollection; j++) 
                {
                    if (Convert.ToBoolean(i & (1 << j)))
                    {
                        elementList.Add(collection.ElementAt(j));
                    }
                }

                resultList.Add(new List<T>(elementList));
                elementList.Clear();
            }

            return resultList;
        }

        /* генерация всех возможных перестановок (без повторений) из
        элементов входного перечисления:
            Входное перечисление: [1, 2, 3]
            Выходное перечисление: [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1] ] */

        public static IEnumerable<IEnumerable<T>> GetGenerationPermutationWithoutElementRepetition<T>(
            this IEnumerable<T>? collection, IEqualityComparer<T>? comparer)
        {

            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection), "collection shouldn't be null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer), "comparer shouldn't be null.");

            }

            ThrowIfNotDistinct(collection, comparer);

            return collection.GenerationPermutationWithoutElementRepetition();
        }

        public static IEnumerable<IEnumerable<T>> GenerationPermutationWithoutElementRepetition<T>(
            this IEnumerable<T> collection)
        {
            if (collection.Count() == 0) 
            {
                yield return new List<T>();
            }

            foreach (var i in collection)
            {
                var next = collection.Where(l => !(l.Equals(i))).ToList();
                foreach (var perm in GenerationPermutationWithoutElementRepetition(next))
                {
                    yield return (new List<T> { i }).Concat(perm);
                }
            }

            

            
        }
    }
}