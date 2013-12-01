using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler75
{
    class Program
    {

        static Tuple<int, int, int> Multiply(int[] matrix, Tuple<int, int, int> vector)
        {
            var x = matrix[0] * vector.Item1 + matrix[1] * vector.Item2 + matrix[2] * vector.Item3;
            var y = matrix[3] * vector.Item1 + matrix[4] * vector.Item2 + matrix[5] * vector.Item3;
            var z = matrix[6] * vector.Item1 + matrix[7] * vector.Item2 + matrix[8] * vector.Item3;
            return Tuple.Create(x, y, z);
        }

        static Tuple<int, int, int> ANext(Tuple<int,int,int> vector)
        {
            int [] A = {1,-2,2,
                        2,-1,2,
                        2,-2,3};
            return Multiply(A, vector);
        }

        static Tuple<int, int, int> BNext(Tuple<int, int, int> vector)
        {
            int[] B = {1,2,2,
                        2,1,2,
                        2,2,3};
            return Multiply(B, vector);
        }

        static Tuple<int, int, int> CNext(Tuple<int, int, int> vector)
        {
            int[] C = {-1,2,2,
                        -2,1,2,
                        -2,2,3};
            return Multiply(C, vector);
        }

        static IEnumerable<Tuple<int,int,int>> PythagoreanTree(int upto)
        {
            Queue<Tuple<int, int, int>> nodes = new Queue<Tuple<int, int, int>>();
            nodes.Enqueue(Tuple.Create(3, 4, 5));
            while (nodes.Count > 0)
            {
                var next = nodes.Dequeue();
                yield return next;

                if (next.Item1 + next.Item2 + next.Item3 <= upto)
                {
                    nodes.Enqueue(ANext(next));
                    nodes.Enqueue(BNext(next));
                    nodes.Enqueue(CNext(next));
                }
            }
        }

        static int Euler75()
        {
            int N = 1500000;
            Dictionary<int, int> lengthToNumSolutions = new Dictionary<int, int>();
            var primitives = PythagoreanTree(N).ToList();
            foreach (var p in primitives)
            {
                int perimeter = p.Item1 + p.Item2 + p.Item3;
                for (int i = 1; perimeter*i <= N; i++)
                {
                    var key = perimeter * i;
                    if (lengthToNumSolutions.ContainsKey(key))
                    {
                        lengthToNumSolutions[key]++;
                    }
                    else
                    {
                        lengthToNumSolutions[key] = 1;
                    }
                }
            }
            int answer = lengthToNumSolutions.Where(kvp => kvp.Value == 1).Count();
            return answer;
        }

        static void Main(string[] args)
        {
            //MakePrimeFactors();
            //var firstFew = PythagoreanTree(1500000).ToList();
            Stopwatch s = new Stopwatch();
            s.Start();
            var answer = Euler75();
            s.Stop();
            Console.WriteLine("{0} is the answer, took {1} seconds to compute.", answer, s.Elapsed.TotalSeconds);
            Console.ReadLine();
        }
    }
}
