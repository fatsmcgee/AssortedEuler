using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotientPermutations
{
    class Program
    {
        static List<ulong> totients;
        static List<List<int>> primeFactors;

        static void MakeTotients()
        {
            var isPrime = Enumerable.Repeat(true, 10000000).ToArray();

            primeFactors = new List<List<int>>(isPrime.Length);

            for (int i = 0; i < isPrime.Length; i++)
            {
                primeFactors.Add(new List<int>());
            }

            isPrime[0] = isPrime[1] = false;
            for (int i = 2; i < isPrime.Length; i++)
            {
                if(!isPrime[i]){
                    continue;
                }

                primeFactors[i].Add(i);

                for (int knockout = i * 2; knockout < isPrime.Length; knockout += i)
                {
                    isPrime[knockout] = false;
                    primeFactors[knockout].Add(i);
                }
            }

            Func<List<int>, int, ulong> factorIndexToTotient = (factors, i) =>
            {
                if (isPrime[i])
                {
                    return (ulong)(i - 1);
                }
                ulong totient = (ulong)i;
                foreach (var prime in factors)
                {
                    totient *= (ulong)(prime - 1);
                    totient /= (ulong)prime;
                }
                return totient;
            };
            totients = primeFactors.Select(factorIndexToTotient).ToList();
        }

        static bool IsPerm(ulong a, ulong b)
        {
            return a.ToString().OrderBy(c => c).SequenceEqual(b.ToString().OrderBy(c => c));
        }

        static long Euler72(int limit)
        {
            return totients.Skip(1).Take(limit).Sum(u => (long)u) - 1;
        }


        static IEnumerable<int> GetCoprimes(int denom)
        {
            for (int i = 1; i < denom; i++)
            {
                if (primeFactors[denom].All(j => i % j != 0))
                {
                    yield return i;
                }
            }
        }
        static long Euler73(int limit, double moreThan, double lessThan)
        {

            var denoms = Enumerable.Range(1, limit);
            var fractions = from d in denoms
                            from coPrime in GetCoprimes(d)
                            select (double)coPrime / (double)d;

            var answer = fractions.Where(f => f > moreThan && f < lessThan);
            return answer.Count();
        }

        static int Euler70()
        {
           
            var perms = totients.Select((t,i) => Tuple.Create(i,t)).Where(tup => IsPerm((ulong)tup.Item1, tup.Item2)).ToList();
            var best = perms.OrderBy(t => (double)t.Item1 / (double)t.Item2);
            var ans =  best.First(t => t.Item1 > 1).Item1;

            return ans;
        }

        static void Main(string[] args)
        {
            MakeTotients();

            var euler73 = Euler73(12000, 1.0 / 3.0, 1.0 / 2.0);
            var euler72 = Euler72(1000000);
            var result = Euler70();
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
