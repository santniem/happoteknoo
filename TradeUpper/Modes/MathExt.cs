using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace TradeUpper.Modes
{
    internal static class MathExt
    {

        public static IEnumerable<IEnumerable<T>> GetKCombs<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return list.GetKCombs(length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        public static IEnumerable<IEnumerable<T>>
  GetPermutationsWithRept<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return list.GetPermutationsWithRept(length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        public static BigInteger getKCombinationCount(int rr, int nn)
        {
            BigInteger r = rr;
            BigInteger n = nn;
            var a = n.Factorial();
            var b = r.Factorial() * (n - r).Factorial();
            var c = a / b;
            return c;
        }
        public static BigInteger Factorial(this BigInteger num)
        {
            BigInteger n = num;
            for (BigInteger i = n - 1; i > 0; i--)
            {
                n *= i;
            }
            return n;
        }
    }

}
