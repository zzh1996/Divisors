using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Divisors
{
    public class Program
    {
        static void Main(string[] args)
        {
            BigInteger n;
            try
            {
                n = BigInteger.Parse(args[0]);
            }
            catch
            {
                Console.WriteLine("Usage: Divisors [number]");
                return;
            }

            if (n < 1)
            {
                Console.WriteLine("number must be a positive integer");
                return;
            }

            Stopwatch watch = Stopwatch.StartNew();
            // core algorithm starts
            List<BigInteger> divisors = Divisors(n);
            // core algorithm ends
            watch.Stop();
            long time = watch.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + " ms");

            foreach (BigInteger d in divisors)
            {
                // add one space every three digits
                Console.WriteLine(String.Format("{0:n0}", d).Replace(",", " "));
            }
        }

        // list all sorted divisors of n
        public static List<BigInteger> Divisors(BigInteger n)
        {
            List<BigInteger> divisors = new List<BigInteger>();
            List<KeyValuePair<BigInteger, int>> primes = Factor(n).ToList();
            int[] index = new int[primes.Count];
            while (true)
            {
                // calculate product of the primes at the current combination
                BigInteger prod = 1;
                for (int i = 0; i < primes.Count; i++)
                    prod *= BigInteger.Pow(primes[i].Key, index[i]);
                divisors.Add(prod);

                // update indices, make indices loop from (0,0,...) to (count1,count2...)
                int j = 0;
                while (j < primes.Count && index[j] == primes[j].Value)
                    index[j++] = 0;
                if (j == primes.Count)
                    break;
                else
                    index[j]++;
            }
            divisors.Sort();
            return divisors;
        }

        // get prime list and counts [(prime1,count1),(prime2,count2)...] of n
        public static Dictionary<BigInteger, int> Factor(BigInteger n)
        {
            Dictionary<BigInteger, int> primes = new Dictionary<BigInteger, int>();
            while (!PrimeTest(n) && !n.IsOne)
            {
                // get one factor of n
                BigInteger factor = GetOneFactor(n);
                // get prime factors
                Dictionary<BigInteger, int> p = Factor(factor);
                // add these primes to prime counter
                foreach (KeyValuePair<BigInteger, int> pair in p)
                {
                    if (primes.ContainsKey(pair.Key))
                        primes[pair.Key] += pair.Value;
                    else
                        primes[pair.Key] = pair.Value;
                }
                n /= factor;
            }
            if (!n.IsOne) // the last factor is n itself
            {
                if (primes.ContainsKey(n))
                    primes[n] += 1;
                else
                    primes[n] = 1;
            }
            return primes;
        }

        // Pollard's rho algorithm
        // Implemented by myself
        public static BigInteger GetOneFactor(BigInteger n)
        {
            BigInteger factor = 1;
            Int64 loop_cnt = 2;
            BigInteger x = 2, x2 = 2;
            while (factor.IsOne)
            {
                for (Int64 i = 1; i < loop_cnt && factor.IsOne; i++)
                {
                    x = (x * x + 1) % n;
                    if (x != x2)
                        factor = BigInteger.GreatestCommonDivisor(BigInteger.Abs(x - x2), n);
                    else
                        x++;
                }
                loop_cnt <<= 1;
                x2 = x;
            }
            return factor;
        }

        // Miller–Rabin primality test
        // code from https://rosettacode.org/wiki/Miller%E2%80%93Rabin_primality_test#C.23
        // I made some little changes
        public static bool PrimeTest(BigInteger n, int certainty = 10)
        {
            if (n == 2 || n == 3)
                return true;
            if (n < 2 || n % 2 == 0)
                return false;

            BigInteger d = n - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            Random rng = new Random();
            byte[] bytes = new byte[n.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    rng.NextBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= n - 2);

                BigInteger x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }

            return true;
        }
    }
}
