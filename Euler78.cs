using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Euler78
{
    class Program
    {
        
        static IEnumerable<int> PentagonalNumbers()
        {
            int i = 1;
            while (true)
            {
                yield return i * (3 * i - 1) / 2;
                if (i > 0)
                {
                    i *= -1;
                }
                else
                {
                    i *= -1;
                    i++;
                }
            }
        }

        static int Euler78()
        {
            var countingNumbers = new List<BigInteger> {1,1};
            int i = 2;
            while (true)
            {
                //only %1000000 is needed, but use BigInteger for the sake of curiosity
                BigInteger pOfN = 0;
                int idx = 0;
                foreach (var p in PentagonalNumbers())
                {
                    int subIdx = i - p;
                    if (subIdx < 0)
                        break;

                    pOfN += countingNumbers[subIdx] * ((idx / 2) % 2 == 0 ? 1 : -1);
                    
                    idx++;
                }

                countingNumbers.Add(pOfN);

                if (pOfN%1000000 == 0)
                {
                    return i;
                }

                i++;
            }
        }

        static void Main(string[] args)
        {
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var answer = Euler78();
            stopwatch.Stop();
            Console.WriteLine("{0} is the solution. Took {1} seconds to compute.", answer,
                              stopwatch.Elapsed.TotalSeconds);
            Console.ReadLine();
        }
    }
}
