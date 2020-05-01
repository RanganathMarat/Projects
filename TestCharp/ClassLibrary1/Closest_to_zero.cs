using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestToZero
{
    public static class Closest_to_zero
    {
        public static int ClosestNumberToZero(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                throw new ArgumentException();
            }

            List<int> postiveNumbers = numbers.Select(x => Math.Abs(x)).ToList();
            postiveNumbers.Sort();

            return numbers.Contains(postiveNumbers[0]) ? postiveNumbers[0] : -postiveNumbers[0];
        }

    }
}
