
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Test
{
    [TestClass]
    public class AdventOfCode2017Day11Test
    {
        [TestMethod]
        public void TestSolve()
        {
            var result = AdventOfCode2017Day11.Solve("Day11Data.txt");
            Assert.AreEqual(682, result.Steps);
            Assert.AreEqual(1406, result.MaxSteps);
        }

        [TestMethod]
        public void TestFindSteps()
        {
            var result = AdventOfCode2017Day11.ProcessSteps(new List<string> {"ne", "ne", "ne"});
            Assert.AreEqual(3, result.Position.X);
            Assert.AreEqual(1.5, result.Position.Y);
            var steps = result.Position.FindSteps().ToList();
            Assert.AreEqual(3, steps.Count);
            Assert.AreEqual("ne", steps[0]);
            Assert.AreEqual("ne", steps[1]);
            Assert.AreEqual("ne", steps[2]);

            result = AdventOfCode2017Day11.ProcessSteps(new List<string> { "ne", "ne", "sw", "sw" });
            Assert.AreEqual(0, result.Position.X);
            Assert.AreEqual(0, result.Position.Y);
            steps = result.Position.FindSteps().ToList();
            Assert.AreEqual(0, steps.Count);

            result = AdventOfCode2017Day11.ProcessSteps(new List<string> { "ne", "ne", "s", "s" });
            Assert.AreEqual(2, result.Position.X);
            Assert.AreEqual(-1, result.Position.Y);
            steps = result.Position.FindSteps().ToList();
            Assert.AreEqual(2, steps.Count);
            Assert.AreEqual("se", steps[0]);
            Assert.AreEqual("se", steps[1]);

            result = AdventOfCode2017Day11.ProcessSteps(new List<string> { "se", "sw", "se", "sw", "sw" });
            //Assert.AreEqual(2, pos.X);
            //Assert.AreEqual(-1, pos.Y);
            steps = result.Position.FindSteps().ToList();
            Assert.AreEqual(3, steps.Count);
            Assert.AreEqual("sw", steps[0]);
            Assert.AreEqual("s", steps[1]);
            Assert.AreEqual("s", steps[2]);
        }
    }
}
