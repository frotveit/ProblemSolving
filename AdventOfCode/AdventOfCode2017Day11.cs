

using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class AdventOfCode2017Day11
    {
        public static ProcessResult Solve(string filename)
        {
            var input = FileUtil.GetFileLine(filename);
            var inputs = StringUtil.SplitOnComma(input);

            var result = ProcessSteps(inputs);
            return result;
        }
        
        public static ProcessResult ProcessSteps(IEnumerable<string> steps)
        {
            ProcessResult result = new ProcessResult();
            
            foreach (var step in steps)
            {
                result.Position.DoStep(step);
                result.Steps = result.Position.FindStepsCount();
                if (result.Steps > result.MaxSteps)
                {
                    result.MaxSteps = result.Steps;
                }
            }
            return result;
        }

        public class ProcessResult
        {
            public Position Position = new Position();
            public int MaxSteps = 0;
            public int Steps;
        }

        public class Position
        {
            public int X = 0;
            public double Y = 0;

            public bool Equal(Position pos)
            {
                return X == pos.X && Y == pos.Y;
            }

            public bool NotEqual(Position pos)
            {
                return Equal(pos) == false;
            }

            public void DoStep(string step)
            {
                switch (step)
                {
                    case "n":
                        Y += 1;
                        break;
                    case "ne":
                        X += 1;
                        Y += 0.5;
                        break;
                    case "se":
                        X += 1;
                        Y -= 0.5;
                        break;
                    case "s":
                        Y -= 1;
                        break;
                    case "sw":
                        X -= 1;
                        Y -= 0.5;
                        break;
                    case "nw":
                        X -= 1;
                        Y += 0.5;
                        break;
                }
            }

            public int FindStepsCount()
            {
                return FindSteps().Count();
            }

            public IEnumerable<string> FindSteps()
            {
                List<string> steps = new List<string>();
                Position cur = new Position();
                while (cur.NotEqual(this))
                {
                    if (cur.X == X)
                    {
                        if (cur.Y < Y)
                        {
                            cur.Y += 1;
                            steps.Add("n");
                        }
                        else
                        {
                            cur.Y -= 1;
                            steps.Add("s");
                        }
                    }
                    else if (cur.X < X)
                    {
                        if (cur.Y < Y)
                        {
                            cur.X += 1;
                            cur.Y += 0.5;
                            steps.Add("ne");
                        }
                        else
                        {
                            cur.X += 1;
                            cur.Y -= 0.5;
                            steps.Add("se");
                        }
                    }
                    else
                    {
                        if (cur.Y < Y)
                        {
                            cur.X -= 1;
                            cur.Y += 0.5;
                            steps.Add("nw");
                        }
                        else
                        {
                            cur.X -= 1;
                            cur.Y -= 0.5;
                            steps.Add("sw");
                        }
                    }
                }
                return steps;
            }
        }

        

        

        
    }
}
