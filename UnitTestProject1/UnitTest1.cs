using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisors;
using System.Numerics;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDivisors()
        {
            CollectionAssert.AreEqual(Program.Divisors(10086), new List<BigInteger> {
                1, 2, 3, 6, 41, 82, 123, 246, 1681, 3362, 5043, 10086
            });
            CollectionAssert.AreEqual(Program.Divisors(1), new List<BigInteger> {
                1
            });
            CollectionAssert.AreEqual(Program.Divisors(2), new List<BigInteger> {
                1, 2
            });
            CollectionAssert.AreEqual(Program.Divisors(3), new List<BigInteger> {
                1, 3
            });
            CollectionAssert.AreEqual(Program.Divisors(4), new List<BigInteger> {
                1, 2, 4
            });
            CollectionAssert.AreEqual(Program.Divisors(36), new List<BigInteger> {
                1, 2, 3, 4, 6, 9, 12, 18, 36
            });
            CollectionAssert.AreEqual(Program.Divisors(1048576), new List<BigInteger> {
                1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576
            });
            CollectionAssert.AreEqual(Program.Divisors(2000000014), new List<BigInteger> {
                1, 2, 1000000007, 2000000014
            });
            CollectionAssert.AreEqual(Program.Divisors(BigInteger.Parse("100841008510086100871008810089")), new List<BigInteger> {
                BigInteger.Parse("1"),
                BigInteger.Parse("3"),
                BigInteger.Parse("96110977499"),
                BigInteger.Parse("288332932497"),
                BigInteger.Parse("349738087969314137"),
                BigInteger.Parse("1049214263907942411"),
                BigInteger.Parse("33613669503362033623669603363"),
                BigInteger.Parse("100841008510086100871008810089"),
            });
        }
    }
}
