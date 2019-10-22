using AdventOfCode.Helpers;

namespace AdventOfCode._2017
{
    public class AdventOfCode2017Day9
    {
        public class Result
        {
            public int Score;
            public int GarbageCount;
        }

        public static Result Score(string stream)
        {
            int level = 0;
            int score = 0;
            bool inGarbage = false;
            bool ignoreNext = false;
            int garbageCount = 0;

            foreach (var item in stream)
            {
                if (ignoreNext)
                {
                    ignoreNext = false;
                    continue;
                }
                if (item == '!')
                {
                    ignoreNext = true;
                    continue;
                }

                if (item == '<' && inGarbage == false)
                {
                    inGarbage = true;
                    continue;
                }
                if (item == '>') inGarbage = false;
                if (inGarbage) garbageCount++;

                if (inGarbage == false)
                {
                    if (item == '{') level++;
                    if (item == '}')
                    {
                        score += level;
                        level--;
                    }
                }
            }

            return new Result
            {
                Score = score,
                GarbageCount = garbageCount
            };
        }

        public static Result Solve(string filename)
        {
            string data = FileUtil.Get2017FileLine(filename);
            return Score(data);
        }

    }
}
