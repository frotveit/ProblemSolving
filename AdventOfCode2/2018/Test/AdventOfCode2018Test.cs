using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2._2018.Test
{
    [TestClass]
    public class AdventOfCode2018Test
    {
        [TestMethod]
        public void Day1_CalculatesFrequency()
        {
            var data = new List<string> { "+1", "-2", "+3", "+1" };
            Assert.AreEqual(3, Day1.CalculateFrequency(0, data)); 
            
            data = new List<string> { "+1", "+1", "+1" };
            Assert.AreEqual(3, Day1.CalculateFrequency(0, data)); 
            
            data = new List<string> { "+1", "+1", "-2" };
            Assert.AreEqual(0, Day1.CalculateFrequency(0, data)); 
            
            data = new List<string> { "-1", "-2", "-3" };
            Assert.AreEqual(-6, Day1.CalculateFrequency(0, data));

            var filedata = FileUtil.Get2018File("Day1.txt");
            Assert.AreEqual(510, Day1.CalculateFrequency(0, filedata));
        }

        [TestMethod]
        public void Day1_CalculatesRecurringFrequency()
        {
            var data = new List<string> { "+1", "-2", "+3", "+1" };
            Assert.AreEqual(2, Day1.CalculateRecurringFrequency(0, data));

            data = new List<string> { "+1", "-1" };
            Assert.AreEqual(0, Day1.CalculateRecurringFrequency(0, data));

            data = new List<string> { "+3", "+3", "+4", "-2", "-4" };
            Assert.AreEqual(10, Day1.CalculateRecurringFrequency(0, data));

            data = new List<string> { "-6", "+3", "+8", "+5", "-6" };
            Assert.AreEqual(5, Day1.CalculateRecurringFrequency(0, data));

            data = new List<string> { "+7", "+7", "-2", "-7", "-4" };
            Assert.AreEqual(14, Day1.CalculateRecurringFrequency(0, data));
            
            var filedata = FileUtil.Get2018File("Day1.txt");
            Assert.AreEqual(69074, Day1.CalculateRecurringFrequency(0, filedata));
        }


        [TestMethod]
        public void Day2_CountRow()
        {
            var count = Day2.CountRow("abcdef");
            Assert.AreEqual(6, count.Count);
            Assert.AreEqual(1, count['a']);
        }

        [TestMethod]
        public void Day2_CalculateRowCount()
        {
            var count = Day2.CalculateRowCount("abcdef");
            Assert.AreEqual(0, count.TwoCount);
            Assert.AreEqual(0, count.ThreeCount);

            count = Day2.CalculateRowCount("bababc");
            Assert.AreEqual(1, count.TwoCount);
            Assert.AreEqual(1, count.ThreeCount);

            count = Day2.CalculateRowCount("abbcde");
            Assert.AreEqual(1, count.TwoCount);
            Assert.AreEqual(0, count.ThreeCount);

            count = Day2.CalculateRowCount("abcccd");
            Assert.AreEqual(0, count.TwoCount);
            Assert.AreEqual(1, count.ThreeCount);

            count = Day2.CalculateRowCount("aabcdd");
            Assert.AreEqual(1, count.TwoCount);
            Assert.AreEqual(0, count.ThreeCount);

            count = Day2.CalculateRowCount("abcdee");
            Assert.AreEqual(1, count.TwoCount);
            Assert.AreEqual(0, count.ThreeCount);

            count = Day2.CalculateRowCount("ababab");
            Assert.AreEqual(0, count.TwoCount);
            Assert.AreEqual(1, count.ThreeCount);
        }
        
        [TestMethod]
        public void Day2_CalculateTotalCount()
        {
            var res = Day2.CalculateTotalCount(new List<string>
            {
                "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab"
            });
            Assert.AreEqual(4, res.TwoCount);
            Assert.AreEqual(3, res.ThreeCount);
        }

        [TestMethod]
        public void Day2_CalculateChecksum()
        {
            var res = Day2.CalculateChecksum(new List<string>
            {
                "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab"
            });
            Assert.AreEqual(12, res);

            var filedata = FileUtil.Get2018File("Day2.txt");
            Assert.AreEqual(7904, Day2.CalculateChecksum(filedata));
        }

        [TestMethod]
        public void Day2_2_DifferByOne()
        {
            Assert.AreEqual(null, Day2Part2.DifferByOne("abcde", "axcye"));
            Assert.AreEqual("fgij", Day2Part2.DifferByOne("fghij", "fguij"));
        }

        [TestMethod]
        public void Day2_2_Calculate()
        {
            var input1 = new List<string>
            {
                "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz"
            };
            var result1 = Day2Part2.Calculate(input1);

            Assert.AreEqual(1, result1.Count());
            Assert.AreEqual("fgij", result1.First());

            var input2 = FileUtil.Get2018File("Day2.txt");

            var result2 = Day2Part2.Calculate(input2);

            Assert.AreEqual(1, result2.Count());
            Assert.AreEqual("wugbihckpoymcpaxefotvdzns", result2.First());

        }

        [TestMethod]
        public void Day3_Parse()
        {
            var res1 = Day3.Parse("#1 @ 1,3: 4x4"); 
            var res2 = Day3.Parse("#2 @ 3,1: 4x4"); 
            var res3 = Day3.Parse("#3 @ 5,5: 2x2");

            Assert.AreEqual(1, res1.Id);
            Assert.AreEqual(1, res1.Location.X); 
            Assert.AreEqual(3, res1.Location.Y); 
            Assert.AreEqual(4, res1.Size.X); 
            Assert.AreEqual(4, res1.Size.Y);
        }

        [TestMethod]
        public void Day3_Calculate()
        {
            var input1 = new List<string>(){ "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };
            Assert.AreEqual(4, Day3.Calculate(8, 8, input1).OverlappingCount);

            var input2 = FileUtil.Get2018File("Day3.txt");
            Assert.AreEqual(110389, Day3.Calculate(1000, 1000, input2).OverlappingCount);
        }

        [TestMethod]
        public void Day3_CalculatePart2()
        {
            var input1 = new List<string>() { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };
            Assert.AreEqual(1, Day3.Calculate(8, 8, input1).NonOverlappingClaims.Count()); 
            Assert.AreEqual(3, Day3.Calculate(8, 8, input1).NonOverlappingClaims.First());

            var input2 = FileUtil.Get2018File("Day3.txt");
            Assert.AreEqual(1, Day3.Calculate(1000, 1000, input2).NonOverlappingClaims.Count()); 
            Assert.AreEqual(552, Day3.Calculate(1000, 1000, input2).NonOverlappingClaims.First());
        }

    }
}



