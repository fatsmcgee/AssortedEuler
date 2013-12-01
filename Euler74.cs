using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler74
{
    class Program
    {
        static Dictionary<ulong, ulong> sumFacDigitsCache = new Dictionary<ulong, ulong>();
        static ulong[] factorials = { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };

        static ulong GetSumFactDigits(ulong n)
        {
            ulong result;
            if(!sumFacDigitsCache.TryGetValue(n,out result)){
                result = 0;
                while (n > 0)
                {
                    result += factorials[n % 10];
                    n /= 10;
                }
                sumFacDigitsCache[n] = result;
            }
            return result;
        }

        static int GetChainLength(ulong seed)
        {
            int length = 1;
            HashSet<ulong> seen = new HashSet<ulong> { seed };
            while (true)
            {
                seed = GetSumFactDigits(seed);
                if (!seen.Contains(seed))
                {
                    length++;
                    seen.Add(seed);
                }
                else
                {
                    break;
                }
            }
            return length;
        }
        static void Main(string[] args)
        {
            var chainLengths = Enumerable.Range(1, 999999).Select(i => GetChainLength((ulong)i)).ToList();
            var answer = chainLengths.Where(cl => cl == 60).Count();
            Console.WriteLine(answer);
            Console.ReadKey();
        }
    }
}
