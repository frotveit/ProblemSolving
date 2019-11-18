

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Test
{
    [TestClass]
    public class TestFSharp
    {
        [TestMethod]
        public void TestSimpleClass()
        {
            Assert.AreEqual(3, new SimpleClass.ASimpleClass().Get3());
        }
    }
}
