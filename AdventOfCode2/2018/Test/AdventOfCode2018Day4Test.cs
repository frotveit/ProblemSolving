using System.Linq;
using AdventOfCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2._2018.Test
{
    [TestClass]
    public class AdventOfCode2018Day4Test
    {
        [TestMethod]
        public void Day4_Process()
        {
            var filedata = FileUtil.Get2018File("Day4-1.txt");

            var rader = Day4.Parse(filedata);
            var skift = Day4.Process(rader);

            Assert.AreEqual(5, skift.Count());
            Assert.AreEqual(2, skift.First().Pauser.Count);
            Assert.AreEqual(4, skift.Count(s => s.Pauser.Count == 1));
        }
        
        [TestMethod]
        public void Day4_Parse_SkalParseTidspunkt()
        {
            var rad = Day4.Parse("[1518-11-01 00:25]...");

            Assert.AreEqual(1518, rad.Tidspunkt.Year);
            Assert.AreEqual(11, rad.Tidspunkt.Month);
            Assert.AreEqual(1, rad.Tidspunkt.Day);
            Assert.AreEqual(0, rad.Tidspunkt.Hour);
            Assert.AreEqual(25, rad.Tidspunkt.Minute);
            Assert.AreEqual(0, rad.Tidspunkt.Second);
        }

        [TestMethod]
        public void Day4_Parse()
        {
            var rad = Day4.Parse("[1518-11-01 00:00] Guard #10 begins shift");

            Assert.AreEqual(1518, rad.Tidspunkt.Year);
            Assert.AreEqual(Day4.RadType.VaktStart, rad.Type);
            Assert.AreEqual(10, rad.VaktId);

            rad = Day4.Parse("[1518-11-01 00:05] falls asleep");

            Assert.AreEqual(1518, rad.Tidspunkt.Year);
            Assert.AreEqual(Day4.RadType.PauseStart, rad.Type);
            Assert.AreEqual(0, rad.VaktId); 
            
            rad = Day4.Parse("[1518-11-01 00:25] wakes up");

            Assert.AreEqual(1518, rad.Tidspunkt.Year);
            Assert.AreEqual(Day4.RadType.PauseSlutt, rad.Type);
            Assert.AreEqual(0, rad.VaktId);
        }

        [TestMethod]
        public void Day4_ParseVaktId()
        {
            Assert.AreEqual("#10", Day4.ParseVaktId("[1518-11-01 00:00] Guard #10 begins shift"));
        }

        [TestMethod]
        public void Day4_FinnVaktId()
        {
            Assert.AreEqual(10, Day4.FinnVaktId("[1518-11-01 00:00] Guard #10 begins shift"));
        }


    }
}



