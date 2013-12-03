using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Euler80
{
    class Program
    {
        static int Euler80()
        {
            HashSet<int> squares = new HashSet<int> { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100 };
            return Enumerable.Range(1, 100).Where(i => !squares.Contains(i))
                .Select(i => First100DigitsOfRoot(i).Sum()).Sum();

        }
        static List<int> First100DigitsOfRoot(int n)
        {
            //uses this method for manual calculation of square rotos
            //http://www.homeschoolmath.net/teaching/square-root-algorithm.php
            List<int> digits = new List<int>();

            BigInteger top = 0;
            BigInteger inner = n;
            
            for (int i = 0; i < 100; i++)
            {
                BigInteger divideBy = top * 2 * 10;
                var nextDigit = Enumerable.Range(0, 10).Select(d => (BigInteger)d)
                    .First(d => d * (divideBy + d) <= inner && (d + 1) * (divideBy + d + 1) > inner);
                digits.Add((int)nextDigit);
                divideBy = divideBy + nextDigit;

                top = top * 10 + nextDigit;
                inner -= divideBy * nextDigit;
                inner *= 100;
            }

            return digits;

        }
        static void Main(string[] args)
        {
            var answer = Euler80();
            Console.WriteLine("Answer is {0}.", answer);
            Console.ReadLine();
        }
    }
}
