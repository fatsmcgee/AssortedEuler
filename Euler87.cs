using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler87
{
    class Program
    {
        static void Main(string[] args)
        {
            var primesTo8K = Enumerable.Range(2, 8000).Where(n => !Enumerable.Range(2, n - 2).Any(i => n % i == 0)).Select(n => (ulong)n).ToList();
            ulong upto = 50000000;
            var sums = from i in primesTo8K
                       let i4 = i * i * i * i
                       where i4 < upto
                       from j in primesTo8K
                       let j3 = j * j * j
                       where j3 < upto
                       from k in primesTo8K
                       let k2 = k * k
                       let sum = i4 + j3 + k2
                       where sum < upto
                       select sum;
            var count = sums.Distinct().Count();
            Console.WriteLine("Answer is " + count);
            Console.ReadLine();
        }
    }
}
