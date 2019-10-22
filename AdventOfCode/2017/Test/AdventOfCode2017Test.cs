using System.Linq;
using AdventOfCode.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode._2017.Test
{

    [TestClass]
    public class AdventOfCode2017Test
    {
        // Day 9

        [TestMethod]
        public void Test_Day9_Score()
        {
            Assert.AreEqual(0, AdventOfCode2017Day9.Score("").Score);
            Assert.AreEqual(1, AdventOfCode2017Day9.Score("{}").Score);
            Assert.AreEqual(3, AdventOfCode2017Day9.Score("{{}}").Score);
            Assert.AreEqual(6, AdventOfCode2017Day9.Score("{{{}}}").Score);
            Assert.AreEqual(5, AdventOfCode2017Day9.Score("{{}{}}").Score);

            Assert.AreEqual(16, AdventOfCode2017Day9.Score("{{{},{},{{}}}}").Score);
            Assert.AreEqual(1, AdventOfCode2017Day9.Score("{<a>,<a>,<a>,<a>}").Score);
            Assert.AreEqual(9, AdventOfCode2017Day9.Score("{{<ab>},{<ab>},{<ab>},{<ab>}}").Score);
            Assert.AreEqual(9, AdventOfCode2017Day9.Score("{{<!!>},{<!!>},{<!!>},{<!!>}}").Score);
            Assert.AreEqual(3, AdventOfCode2017Day9.Score("{{<a!>},{<a!>},{<a!>},{<ab>}}").Score);
                        
            Assert.AreEqual(12803, AdventOfCode2017Day9.Solve("Day9Data.txt").Score);
        }

        [TestMethod]
        public void Test_Day9_GarbageCount()
        {
            Assert.AreEqual(0, AdventOfCode2017Day9.Score("").GarbageCount);
            Assert.AreEqual(0, AdventOfCode2017Day9.Score("<>").GarbageCount);
            Assert.AreEqual(17, AdventOfCode2017Day9.Score("<random characters>").GarbageCount);
            Assert.AreEqual(3, AdventOfCode2017Day9.Score("<<<<>").GarbageCount);
            Assert.AreEqual(2, AdventOfCode2017Day9.Score("<{!>}>").GarbageCount);
            Assert.AreEqual(0, AdventOfCode2017Day9.Score("<!!>").GarbageCount);
            Assert.AreEqual(0, AdventOfCode2017Day9.Score("<!!!>>").GarbageCount);
            Assert.AreEqual(10, AdventOfCode2017Day9.Score("<{o\"i!a,<{i<a>").GarbageCount);

            Assert.AreEqual(6425, AdventOfCode2017Day9.Solve("Day9Data.txt").GarbageCount);
        }

        // Day 8

        [TestMethod]
        public void Test_Day8_ParseLine()
        {
            AdventOfCode2017Day8.Instruction instruction = AdventOfCode2017Day8.ParseLine("b inc 5 if a > 1");
            Assert.AreEqual("b", instruction.Register);
            Assert.AreEqual(AdventOfCode2017Day8.RegisterOperation.Increment, instruction.Operation);
            Assert.AreEqual(5, instruction.Value);
            Assert.AreEqual("a", instruction.Condition.Register);
            Assert.AreEqual(">", instruction.Condition.Condition);
            Assert.AreEqual(1, instruction.Condition.Value);
        }

        [TestMethod]
        public void Test_Day8_Registers_GetMax()
        {
            AdventOfCode2017Day8.Registers registers = new AdventOfCode2017Day8.Registers();
            registers.Data.Add("e", 7);
            registers.Data.Add("f", 11);
            registers.Data.Add("gg", 3);

            Assert.AreEqual(11, registers.GetMax());
        }

        [TestMethod]
        public void Test_Day8_Solve()
        {
            var result = AdventOfCode2017Day8.Solve("Day8Data.txt");
            Assert.AreEqual(1, result.Max);
            Assert.AreEqual(10, result.ProcessMax);

            var fullResult = AdventOfCode2017Day8.Solve("Day8FullData.txt");
            Assert.AreEqual(3880, fullResult.Max);
            Assert.AreEqual(5035, fullResult.ProcessMax);
        }

        // Day 6

        [TestMethod]
        public void Test_Day6BlockRedistributor_GetMaxIndex()
        {
            Assert.AreEqual(2, Day6BlockRedistributor.GetMaxIndex(new [] { 0, 2, 7, 0 }));
        }

        [TestMethod]
        public void Test_Day6BlockDistributionStack_AreEqual()
        {
            var stack = new Day6BlockDistributionStack();

            Assert.IsTrue(stack.AreEqual(new [] { 1, 4, 6, 2 }, new [] { 1, 4, 6, 2 }));

            Assert.IsFalse(stack.AreEqual(new [] { 1, 2, 3 }, new [] { 1, 2, 3, 4 }));
            Assert.IsFalse(stack.AreEqual(new [] { 1, 2, 3, 5 }, new [] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void Test_Day6BlockDistributionStack_Push()
        {
            var stack = new Day6BlockDistributionStack();

            Assert.IsTrue(stack.Push(new [] { 1, 4, 6, 2 }).Success);
            Assert.IsTrue(stack.Push(new [] { 1, 4, 3, 2 }).Success);
            Assert.IsTrue(stack.Push(new [] { 1, 2, 6, 2 }).Success);

            Assert.IsFalse(stack.Push(new [] { 1, 4, 3, 2 }).Success);
        }

        [TestMethod]
        public void Test_Day6BlockRedistributor_RedistributeOnce()
        {
            int[] result = Day6BlockRedistributor.RedistributeOnce(new [] { 0, 2, 7, 0 });
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(4, result[1]);
            Assert.AreEqual(1, result[2]);
            Assert.AreEqual(2, result[3]);

            result = Day6BlockRedistributor.RedistributeOnce(new [] { 2, 4, 1, 2 });
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(2, result[2]);
            Assert.AreEqual(3, result[3]);
        }

        private readonly int[] _day6Data = { 4, 10, 4, 1, 8, 4, 9, 14, 5, 1, 14, 15, 0, 15, 3, 5 };

        [TestMethod]
        public void Test_Day6BlockRedistributor_Redistribute()
        {
            var result = Day6BlockRedistributor.Redistribute(new [] { 0, 2, 7, 0 });
            Assert.AreEqual(5, result.RedisributionCount);
            Assert.AreEqual(4, result.LoopCount);

            var result2 = Day6BlockRedistributor.Redistribute(_day6Data);
            Assert.AreEqual(12841, result2.RedisributionCount);
            Assert.AreEqual(8038, result2.LoopCount);
        }


        // Day 5

        [TestMethod]
        public void Test_Day5()
        {
            int[] data = { 0, 3, 0, 1, -3 };
            Assert.AreEqual(5, Day5Jumper.Solve(data));

            int[] resultingArray = { 2, 5, 0, 1, -2 };
            int i = 0;
            foreach (int r in resultingArray)
            {
                Assert.AreEqual(r, data[i]);
                i++;
            }

            string[] filedata = FileUtil.Get2017File("Day5Data.txt");
            int [] data2 = filedata.Select(int.Parse).ToArray();

            Assert.AreEqual(388611, Day5Jumper.Solve(data2));            
        }

        [TestMethod]
        public void Test_Day5_2()
        {
            int[] data = { 0, 3, 0, 1, -3 };
            Assert.AreEqual(10, Day5Jumper.Solve2(data));

            int[] resultingArray = { 2, 3, 2, 3, -1 };
            int i = 0;
            foreach (int r in resultingArray)
            {
                Assert.AreEqual(r, data[i]);
                i++;
            }


            string[] filedata = FileUtil.Get2017File("Day5Data.txt");
            int[] data2 = filedata.Select(int.Parse).ToArray();

            Assert.AreEqual(27763113, Day5Jumper.Solve2(data2));            
        }

        

        // Day 4

        [TestMethod]
        public void Test_Day4PassphraseAnagramChecker_Solve()
        {
            Assert.AreEqual(167, Day4PassphraseAnagramChecker.Solve("Day4Data.txt"));
        }

        [TestMethod]
        public void Test_Day4PassphraseAnagramChecker_SortLetters()
        {
            Assert.AreEqual("abcde", Day4PassphraseAnagramChecker.SortLetters("abcde"));
            Assert.AreEqual("abcde", Day4PassphraseAnagramChecker.SortLetters("ecdab"));

            Assert.AreEqual("aab", Day4PassphraseAnagramChecker.SortLetters("aab"));
            Assert.AreEqual("aab", Day4PassphraseAnagramChecker.SortLetters("aba"));
            Assert.AreEqual("aab", Day4PassphraseAnagramChecker.SortLetters("baa"));
        }

        [TestMethod]
        public void Test_Day4PassphraseChecker_Solve()
        {
            Assert.AreEqual(477, Day4PassphraseChecker.Solve("Day4Data.txt"));
        }

        [TestMethod]
        public void Test_Day4PassphraseChecker_CheckPhrase()
        {
            Assert.IsTrue(Day4PassphraseChecker.CheckPhrase("aa bb cc dd ee"));
            Assert.IsTrue(Day4PassphraseChecker.CheckPhrase("aa bb cc dd aaa"));
            Assert.IsFalse(Day4PassphraseChecker.CheckPhrase("aa bb cc dd aa"));
        }

        // Day3

        [TestMethod]
        public void Test_Day3SpiralMemorySum_Solve()
        {
            Assert.AreEqual(1, Day3SpiralMemorySum.SolveValueOf(1));
            Assert.AreEqual(1, Day3SpiralMemorySum.SolveValueOf(2));
            Assert.AreEqual(2, Day3SpiralMemorySum.SolveValueOf(3));
            Assert.AreEqual(4, Day3SpiralMemorySum.SolveValueOf(4));
            Assert.AreEqual(5, Day3SpiralMemorySum.SolveValueOf(5));
            Assert.AreEqual(10, Day3SpiralMemorySum.SolveValueOf(6));
            Assert.AreEqual(11, Day3SpiralMemorySum.SolveValueOf(7));
            Assert.AreEqual(23, Day3SpiralMemorySum.SolveValueOf(8));

            Assert.AreEqual(1, Day3SpiralMemorySum.SolveBiggerThan(0));
            Assert.AreEqual(2, Day3SpiralMemorySum.SolveBiggerThan(1));
            Assert.AreEqual(4, Day3SpiralMemorySum.SolveBiggerThan(2));
            Assert.AreEqual(5, Day3SpiralMemorySum.SolveBiggerThan(4));
            Assert.AreEqual(10, Day3SpiralMemorySum.SolveBiggerThan(5));
            Assert.AreEqual(363010, Day3SpiralMemorySum.SolveBiggerThan(361527));
        }

        [TestMethod]
        public void Test_Day3SpiralMemory_Solve()
        {
            Assert.AreEqual(0, Day3SpiralMemory.SolveSteps(1));
            Assert.AreEqual(3, Day3SpiralMemory.SolveSteps(12));
            Assert.AreEqual(2, Day3SpiralMemory.SolveSteps(23));
            Assert.AreEqual(31, Day3SpiralMemory.SolveSteps(1024));
            Assert.AreEqual(326, Day3SpiralMemory.SolveSteps(361527));

        }

        // Day2        
        
        [TestMethod]
        public void Test_Day2Checksum_SolveEven()
        {
            string[] input =
            {
                "5 9 2 8",
                "9 4 7 3",
                "3 8 6 5"
            };
            Assert.AreEqual(9, Day2Checksum.SolveEven(input));
            string[] input3 = FileUtil.Get2017File("Day2Data.txt");
            Assert.AreEqual(242, Day2Checksum.SolveEven(input3));
        }

        [TestMethod]
        public void Test_Day2Checksum_IsEvenlyDivisible()
        {
            Assert.IsTrue(Day2Checksum.IsEvenlyDivisible(8, 2));
            Assert.IsTrue(Day2Checksum.IsEvenlyDivisible(2, 8));

            Assert.IsFalse(Day2Checksum.IsEvenlyDivisible(8, 3));            
        }

        [TestMethod]
        public void Test_Day2Checksum_Solve()
        {
            string[] input =
            {
                "5 1 9 5",
                "7 5 3",
                "2 4 6 8"
            };
            Assert.AreEqual(18, Day2Checksum.Solve1(input));
            string[] input3 = FileUtil.Get2017File("Day2Data.txt");
            Assert.AreEqual(44887, Day2Checksum.Solve1(input3));
        }
        
        // Day 1

        string Day1Input = "818275977931166178424892653779931342156567268946849597948944469863818248114327524824136924486891794739281668741616818614613222585132742386168687517939432911753846817997473555693821316918473474459788714917665794336753628836231159578734813485687247273288926216976992516314415836985611354682821892793983922755395577592859959966574329787693934242233159947846757279523939217844194346599494858459582798326799512571365294673978955928416955127211624234143497546729348687844317864243859238665326784414349618985832259224761857371389133635711819476969854584123589566163491796442167815899539788237118339218699137497532932492226948892362554937381497389469981346971998271644362944839883953967698665427314592438958181697639594631142991156327257413186621923369632466918836951277519421695264986942261781256412377711245825379412978876134267384793694756732246799739464721215446477972737883445615664755923441441781128933369585655925615257548499628878242122434979197969569971961379367756499884537433839217835728263798431874654317137955175565253555735968376115749641527957935691487965161211853476747758982854811367422656321836839326818976668191525884763294465366151349347633968321457954152621175837754723675485348339261288195865348545793575843874731785852718281311481217515834822185477982342271937155479432673815629144664144538221768992733498856934255518875381672342521819499939835919827166318715849161715775427981485233467222586764392783699273452228728667175488552924399518855743923659815483988899924199449721321589476864161778841352853573584489497263216627369841455165476954483715112127465311353411346132671561568444626828453687183385215975319858714144975174516356117245993696521941589168394574287785233685284294357548156487538175462176268162852746996633977948755296869616778577327951858348313582783675149343562362974553976147259225311183729415381527435926224781181987111454447371894645359797229493458443522549386769845742557644349554641538488252581267341635761715674381775778868374988451463624332123361576518411234438681171864923916896987836734129295354684962897616358722633724198278552339794629939574841672355699222747886785616814449297817352118452284785694551841431869545321438468118";

        [TestMethod]
        public void TestDay1CaptchaSolve1()
        {
            Assert.AreEqual(3, Day1Captcha.Solve1("1122"));
            Assert.AreEqual(4, Day1Captcha.Solve1("1111"));
            Assert.AreEqual(0, Day1Captcha.Solve1("1234"));
            Assert.AreEqual(9, Day1Captcha.Solve1("91212129"));
            Assert.AreEqual(1097, Day1Captcha.Solve1(Day1Input));
        }
        [TestMethod]
        public void TestDay1CaptchaSolve1Func()
        {
            Assert.AreEqual(3, Day1Captcha.Solve1Func("1122"));
            Assert.AreEqual(4, Day1Captcha.Solve1Func("1111"));
            Assert.AreEqual(0, Day1Captcha.Solve1Func("1234"));
            Assert.AreEqual(9, Day1Captcha.Solve1Func("91212129"));
            Assert.AreEqual(1097, Day1Captcha.Solve1Func(Day1Input));
        }

        [TestMethod]
        public void TestDay1CaptchaSolve2()
        {
            Assert.AreEqual(6, Day1Captcha.Solve2("1212"));
            Assert.AreEqual(0, Day1Captcha.Solve2("1221"));
            Assert.AreEqual(4, Day1Captcha.Solve2("123425"));
            Assert.AreEqual(12, Day1Captcha.Solve2("123123"));
            Assert.AreEqual(4, Day1Captcha.Solve2("12131415"));
            Assert.AreEqual(1188, Day1Captcha.Solve2(Day1Input));
        }
    }
 
}
