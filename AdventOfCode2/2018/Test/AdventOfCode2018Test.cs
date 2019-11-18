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
    }
}
