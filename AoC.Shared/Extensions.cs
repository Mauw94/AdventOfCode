using System.Text;
using System.Text.RegularExpressions;
using static AoC.Shared.Grid2d;

namespace AoC.Shared
{
    /// <summary>
    /// Extension methods that are used in the solutions for AoC2022.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Creates a list of ints based on an exisiting list, 
        /// taking a start and end parameter 
        /// and then filling the missing numbers in between start and end.
        /// </summary>
        public static List<int> FillList(this List<int> list, int start, int end)
        {
            var fullList = new List<int>();

            for (int i = start; i <= end; i++)
                fullList.Add(i);

            return fullList;
        }

        /// <summary>
        /// Check wether list a contains all items of list b.
        /// </summary>
        public static bool ContainsAllItems<T>(this List<T> a, List<T> b)
            => !b.Except(a).Any();

        /// <summary>
        /// Check wether list a has any overlap oof items with list b.
        /// </summary>
        public static bool HasAnyOverlap<T>(this List<T> a, List<T> b)
            => a.Any(x => b.Contains(x));

        /// <summary>
        /// Convert numbers from a string to an int array.
        /// </summary>
        public static int[] GetNumbersFromString(this string input)
        {
            return Regex.Split(input, @"\D+")
                    .Where(x => x != "")
                    .Select(x => int.Parse(x))
                    .ToArray();
        }

        /// <summary>
        /// Convert numbers from a string to an int array.
        /// </summary>
        public static int[] ToIntArray(this string input, string delimiter = "")
        {
            if (delimiter == "")
            {
                var result = new List<int>();
                foreach (var c in input)
                    if (int.TryParse(c.ToString(), out int n))
                        result.Add(n);

                return result.ToArray();
            }
            else
            {
                return input
                    .Split(delimiter)
                    .Where(n => int.TryParse(n, out int v))
                    .Select(n => Convert.ToInt32(n))
                    .ToArray();
            }
        }

        /// <summary>
        /// Get the product of all the factors in a list.
        /// </summary>
        public static int Product(this IEnumerable<int> input)
        {
            var product = 1;
            foreach (var i in input)
                product *= i;

            return product;
        }

        /// <summary>
        /// Get the product of all the factors in a list of longs.
        /// </summary>
        public static long LongProduct(this IEnumerable<long> input)
        {
            var product = 1L;
            foreach (var i in input)
                product *= i;

            return product;
        }

        /// <summary>
        /// Parses a character that's a digit to its int notation.
        /// </summary>
        public static int ToInt(this char character)
            => char.IsDigit(character)
                ? int.Parse(character.ToString())
                : throw new ArgumentException($"{character} is not a digit.");

        /// <summary>
        /// Convert char to its ascii value.
        /// </summary.
        public static int CharToAscii(this char character)
            => Convert.ToInt32(character);
    }
}