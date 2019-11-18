using System.Collections.Generic;
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
        public void CalculateTotalCount()
        {
            var res = Day2.CalculateTotalCount(new List<string>
            {
                "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab"
            });
            Assert.AreEqual(4, res.TwoCount);
            Assert.AreEqual(3, res.ThreeCount);
        }

        [TestMethod]
        public void CalculateChecksum()
        {
            var res = Day2.CalculateChecksum(new List<string>
            {
                "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab"
            });
            Assert.AreEqual(12, res);

            var filedata = FileUtil.Get2018File("Day2.txt");
            Assert.AreEqual(7904, Day2.CalculateChecksum(filedata));
        }

    }
}

/*

*/
