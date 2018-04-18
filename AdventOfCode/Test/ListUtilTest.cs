using System.Collections.Generic;
using AdventOfCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Test
{
    [TestClass]
    public class ListUtilTest
    {
        [TestMethod]
        public void Test_AreEqual()
        {
            var list = new List<int> {3, 4, 5};

            var list2 = new List<int> { 3, 4 };
            Assert.IsFalse(list.AreEqual(list2));

            list2 = new List<int> { 3, 4, 5, 6 };
            Assert.IsFalse(list.AreEqual(list2));

            list2 = new List<int> { 3, 4, 5 };
            Assert.IsTrue(list.AreEqual(list2));

            list2 = new List<int> { 3, 4, 8 };
            Assert.IsFalse(list.AreEqual(list2));
        }
    }
}
