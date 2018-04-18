using System.Collections.Generic;
using AdventOfCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Test
{
    [TestClass]
    public class AdventOfCode2017Day10Test
    {
        [TestMethod]
        public void Test_Solve()
        {
            var input1 = new List<int> { 3, 4, 1, 5 };
            Assert.AreEqual(12, AdventOfCode2017Day10.Solve(5, input1 ));

            var input2 = new List<int> {227, 169, 3, 166, 246, 201, 0, 47, 1, 255, 2, 254, 96, 3, 97, 144};
            Assert.AreEqual(13760, AdventOfCode2017Day10.Solve(256, input2));
        }

        [TestMethod]
        public void TestSolve2()
        {
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", AdventOfCode2017Day10.Solve2(""));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", AdventOfCode2017Day10.Solve2("AoC 2017"));
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", AdventOfCode2017Day10.Solve2("1,2,3"));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", AdventOfCode2017Day10.Solve2("1,2,4"));

            Assert.AreEqual("2da93395f1a6bb3472203252e3b17fe5", AdventOfCode2017Day10.Solve2("227,169,3,166,246,201,0,47,1,255,2,254,96,3,97,144"));
        }

        [TestMethod]
        public void Test_ConvertToAscii()
        {
            var numbers = AdventOfCode2017Day10.ConvertToAscii("1,2,3");
            Assert.AreEqual(5, numbers.Count);
            Assert.IsTrue(numbers.AreEqual(new List<int> { 49, 44, 50, 44, 51 }));
        }

        [TestMethod]
        public void Test_FormatHash()
        {
            Assert.AreEqual("4007ff", AdventOfCode2017Day10.FormatHash(new List<int> { 64, 7, 255 }));

        }

        [TestMethod]
        public void Test_Hash()
        {
            Assert.AreEqual(64, AdventOfCode2017Day10.Hash(new List<int>{ 65, 27, 9, 1, 4, 3, 40, 50, 91, 7, 6, 0, 2, 5, 68, 22 }));
        }

        [TestMethod]
        public void Test_SplitForHash()
        {
            var numbers = new AdventOfCode2017Day10.NumberList(256);
            var split = AdventOfCode2017Day10.SplitForHash(numbers.Numbers);
            Assert.AreEqual(16, split.Count);
            Assert.AreEqual(16, split[0].Count);
            Assert.AreEqual(16, split[15].Count);
            Assert.AreEqual(0, split[0][0]);
            Assert.AreEqual(15, split[0][15]);
            Assert.AreEqual(16, split[1][0]);
        }

        [TestMethod]
        public void Test_NumberList_Get()
        {
            var numbers = new AdventOfCode2017Day10.NumberList(5);

            var res = numbers.Get(1);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(0, res[0]);

            res = numbers.Get(5);
            Assert.AreEqual(5, res.Count);
            Assert.AreEqual(0, res[0]);
            Assert.AreEqual(4, res[4]);
        }

        [TestMethod]
        public void Test_NumberList_Replace()
        {
            var numbers = new AdventOfCode2017Day10.NumberList(5);

            numbers.Replace(1, new List<int>{8});

            Assert.AreEqual(8, numbers.Numbers[0]);
            Assert.AreEqual(1, numbers.Numbers[1]);

            numbers.Replace(2, new List<int> { 7, 8 });

            Assert.AreEqual(7, numbers.Numbers[0]);
            Assert.AreEqual(8, numbers.Numbers[1]);
            Assert.AreEqual(2, numbers.Numbers[2]);
        }

        [TestMethod]
        public void Test_NumberList_Next()
        {
            var numbers = new AdventOfCode2017Day10.NumberList(5);

            numbers.Next(2);
            Assert.AreEqual(2, numbers.Start);

            numbers.Next(1);
            Assert.AreEqual(4, numbers.Start);

            numbers.Next(1);
            Assert.AreEqual(2, numbers.Start);
        }

        [TestMethod]
        public void Test_NumberList_Process()
        {
            var numbers = new AdventOfCode2017Day10.NumberList(5);

            numbers.Process(3);
            Assert.IsTrue(numbers.Numbers.AreEqual(new List<int> { 2,1,0,3,4 }));
            Assert.AreEqual(3, numbers.Start);
            Assert.AreEqual(1, numbers.Skip);

            numbers.Process(4);
            Assert.IsTrue(numbers.Numbers.AreEqual(new List<int> { 4,3,0,1,2 }));
            Assert.AreEqual(3, numbers.Start);
            Assert.AreEqual(2, numbers.Skip);

            numbers.Process(1);
            Assert.IsTrue(numbers.Numbers.AreEqual(new List<int>{ 4,3,0,1,2 }));
            Assert.AreEqual(1, numbers.Start);
            Assert.AreEqual(3, numbers.Skip);

            numbers.Process(5);
            Assert.IsTrue(numbers.Numbers.AreEqual(new List<int> { 3,4,2,1,0 }));
            Assert.AreEqual(4, numbers.Start);
            Assert.AreEqual(4, numbers.Skip);


        }
    }
}
