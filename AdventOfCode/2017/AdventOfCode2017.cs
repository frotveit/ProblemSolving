using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode._2017
{
    public class AdventOfCode2017
    {
    }

    public class Day6BlockRedistributorResult
    {
        public int RedisributionCount;
        public int LoopCount;
        public int[] Distibution;
    }

    public class Day6BlockDistributionStackPushResult
    {
        public bool Success;
        public int Count;

        public Day6BlockDistributionStackPushResult(bool success, int count)
        {
            Success = success;
            Count = count;
        }
    }

    public class Day6BlockDistributionStack
    {
        public class StackItem
        {
            public int[] Distribution;
            public int StackCount;
        }

        readonly List<StackItem> _stack = new List<StackItem>();
        int _count;

        public Day6BlockDistributionStackPushResult Push(int[] distribution)
        {
            foreach ( var dist in _stack)
            {
                if (AreEqual(dist.Distribution, distribution))
                    return new Day6BlockDistributionStackPushResult(false, dist.StackCount);
            }
            _count++;
            _stack.Add(new StackItem { Distribution = distribution, StackCount = _count });
            return new Day6BlockDistributionStackPushResult(true, _count);
        } 

        public bool AreEqual(int [] dist1, int[] dist2)
        {
            if (dist1.Length != dist2.Length)
                return false;

            for (int i = 0; i < dist1.Length; i++)
            {
                if (dist1[i] != dist2[i])
                    return false;
            }
            return true;
        }
    }

    public class Day6BlockRedistributor
    {
        public static Day6BlockRedistributorResult Redistribute(int[] initialDistibution)
        {
            var stack = new Day6BlockDistributionStack();
            stack.Push(initialDistibution);
            int count = 0;
            var dist = initialDistibution;
            bool finished = false;
            int loopCount = 0;
            while (!finished)
            {
                count++;
                               
                var newDist = RedistributeOnce(dist);
                var result = stack.Push(newDist);
                if (result.Success == false)
                {
                    finished = true;
                    loopCount = count - result.Count +1;
                }
                else
                    dist = newDist;
            }
            return new Day6BlockRedistributorResult { RedisributionCount = count, Distibution = dist, LoopCount = loopCount };
        }

        public static int[] RedistributeOnce(int[] distibution)
        {
            int[] newDist = Copy(distibution);
            var index = GetMaxIndex(distibution);

            int value = distibution[index];
            newDist[index] = 0;

            int i = index;
            while (value > 0)
            {
                i++;
                if (i >= newDist.Length)
                    i = 0;
                newDist[i]++;
                value--;
            }

            return newDist;
        }

        private static int[] Copy(int[] initialDistibution)
        {
            int[] newDist = new int[initialDistibution.Length];
            for (int i = 0; i < initialDistibution.Length; i++)
            {
                newDist[i] = initialDistibution[i];
            }

            return newDist;
        }

        public static int GetMaxIndex(int[] distibution)
        {
            int max = 0;
            int pos = 0;
            for (int i = 0; i < distibution.Length; i++)
            {
                int val = distibution[i];
                if (val > max)
                {
                    max = val;
                    pos = i;
                }
            }
            return pos;
        }
    }


    public class Day5Jumper
    {
        public static int Solve(int[] data)
        {
            int length = data.Length;
            int steps = 0;
            int pos = 0;
            while (pos >= 0 && pos < length)
            {
                int jump = data[pos];
                data[pos]++;
                pos = pos + jump;
                steps++;
            }
            return steps;
        }

        public static int Solve2(int[] data)
        {
            int length = data.Length;
            int steps = 0;
            int pos = 0;
            while (pos >= 0 && pos < length)
            {
                int jump = data[pos];
                if (jump >= 3)
                    data[pos]--;
                else
                    data[pos]++;
                pos = pos + jump;
                steps++;
            }
            return steps;
        }
    }

    public class Day4PassphraseAnagramChecker
    {
        public static int Solve(string filename)
        {
            string[] lines = FileUtil.Get2017File(filename);
            int count = 0;
            foreach (string line in lines)
            {
                if (CheckPhrase(line))
                    count++;
            }
            return count;
        }

        public static bool CheckPhrase(string phrase)
        {
            string[] words = StringUtil.Split(phrase);

            HashSet<string> usedWords = new HashSet<string>();

            foreach (string word in words)
            {
                string sortedWord = SortLetters(word);
                if (usedWords.Contains(sortedWord))
                    return false;
                usedWords.Add(sortedWord);
            }
            return true;
        }

        public static string SortLetters(string word)
        { 
            var orderedCahrs = word.OrderBy(c => c);
            return new string(orderedCahrs.ToArray());
        }

        public static string SortString(string input)
        {            
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }

    public class Day4PassphraseChecker
    {
        public static int Solve(string filename)
        {
            string[] lines = FileUtil.Get2017File(filename);
            int count = 0;
            foreach (string line in lines)
            {
                if (CheckPhrase(line))
                    count++;
            }
            return count;
        }        

        public static bool CheckPhrase(string phrase)
        {
            string[] words = StringUtil.Split(phrase);

            HashSet<string> usedWords = new HashSet<string>();

            foreach (string word in words)
            {
                if (usedWords.Contains(word))
                    return false;
                usedWords.Add(word);
            }
            return true;            
        }        
    }

    public class Day3SpiralMemorySum
    {
        class SpiralMemoryItem
        {
            public int pos;
            public int x;
            public int y;
            public int value;

            public SpiralMemoryItem(SpiralMemoryState state)
            {
                pos = state.pos;
                x = state.curX;
                y = state.curY;
                value = state.value;
            }
        }

        class SpiralMemory
        {
            readonly List<SpiralMemoryItem> _data = new List<SpiralMemoryItem>();

            public void Add(SpiralMemoryState state)
            {
                Add(new SpiralMemoryItem(state));
            }
            public void Add(SpiralMemoryItem item)
            {
                _data.Add(item);
            }

            public SpiralMemoryItem Get(Pos pos)
            {
                return Get(pos.x, pos.y);
            }

            public SpiralMemoryItem Get(int x, int y)
            {
                return _data.FirstOrDefault(d => d.x == x & d.y == y);
            }            
        }

        class Pos
        {
            public int x;
            public int y;
            public Pos() { }
            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        class SpiralMemoryState
        {
            public SpiralMemoryState()
            {
                //memory.Add(new SpiralMemoryItem(this, 1));
            }
            public int pos = 1;
            public int value = 1;
            public int curX = 0;
            public int curY = 0;
            public int direction = 1;

            public int highX = 0;
            public int lowX = 0;
            public int highY = 0;
            public int lowY = 0;

            public SpiralMemory memory = new SpiralMemory();

            public void next()
            {
                memory.Add(this); // Add previous to memory
                pos++;
                increaseCurr();
                value = getValue();                
                updateDirection();
                updateSize();
            }

            public int getValue()
            {
                var value = 0;
                var positions = GetNeighbourPositions(new Pos(this.curX, this.curY));
                foreach (var pos in positions)
                {
                    var item = memory.Get(pos);
                    if (item != null)
                        value += item.value;
                }
                return value;
            }

            public IEnumerable<Pos> GetNeighbourPositions(Pos pos)
            {
                return new List<Pos>()
                {
                    new Pos(pos.x + 1, pos.y),
                    new Pos(pos.x + 1, pos.y + 1),
                    new Pos(pos.x + 1, pos.y - 1),
                    new Pos(pos.x, pos.y + 1),
                    new Pos(pos.x, pos.y - 1),
                    new Pos(pos.x - 1, pos.y),
                    new Pos(pos.x - 1, pos.y + 1),
                    new Pos(pos.x - 1, pos.y - 1),
                };
            }

            public void increaseCurr()
            {
                if (direction == 1)
                    curX++;
                if (direction == 2)
                    curY++;
                if (direction == 3)
                    curX--;
                if (direction == 4)
                    curY--;
            }

            public void updateDirection()
            {
                if (direction == 1 && curX > highX)
                    direction = 2;
                else if (direction == 2 && curY > highY)
                    direction = 3;
                else if (direction == 3 && curX < lowX)
                    direction = 4;
                else if (direction == 4 && curY < lowY)
                    direction = 1;
            }

            public void updateSize()
            {
                if (curX > highX)
                    highX = curX;
                if (curX < lowX)
                    lowX = curX;

                if (curY > highY)
                    highY = curY;
                if (curY < lowY)
                    lowY = curY;
            }


            public int Steps()
            {
                return Math.Abs(curX) + Math.Abs(curY);
            }
        }
        public static int SolveValueOf(int pos)
        {
            SpiralMemoryState state = new SpiralMemoryState();
            while (state.pos < pos)
            {
                state.next();
            }
            return state.value;
        }

        public static int SolveBiggerThan(int value)
        {
            SpiralMemoryState state = new SpiralMemoryState();
            while (state.value <= value)
            {
                state.next();
            }
            return state.value;
        }
    }

    public class Day3SpiralMemory
    {
        class SpiralMemoryState
        {
            public int value = 1;
            public int curX = 0;
            public int curY = 0;
            public int direction = 1;

            public int highX = 0;
            public int lowX = 0;
            public int highY = 0;
            public int lowY = 0;

            public void Next()
            {
                value++;
                IncreaseCurr();
                UpdateDirection();
                UpdateSize();
            }

            public void IncreaseCurr()
            {
                if (direction == 1)
                    curX++;
                if (direction == 2)
                    curY++;
                if (direction == 3)
                    curX--;
                if (direction == 4)
                    curY--;
            }

            public void UpdateDirection()
            {
                if (direction == 1 && curX > highX)
                    direction = 2;
                else if (direction == 2 && curY > highY)
                    direction = 3;
                else if (direction == 3 && curX < lowX)
                    direction = 4;
                else if (direction == 4 && curY < lowY)
                    direction = 1;
            }

            public void UpdateSize()
            {
                if (curX > highX)
                    highX = curX;
                if (curX < lowX)
                    lowX = curX;

                if (curY > highY)
                    highY = curY;
                if (curY < lowY)
                    lowY = curY;
            }


            public int Steps()
            {
                return Math.Abs(curX) + Math.Abs(curY);
            }
        }
        public static int SolveSteps(int value)
        {
            SpiralMemoryState state = new SpiralMemoryState();
            while (state.value < value)
                state.Next();
            return state.Steps();
        }
    }

    public class Day2Checksum
    {
        public static int SolveEven(string[] numbers)
        {
            int checksum = 0;
            foreach (string line in numbers)
            {
                string[] items = StringUtil.Split(line);

                bool hit = false;
                int result = 0;
                for (int i = 0; i < items.Length - 1; i++)
                {
                    string item = items[i];
                    int val = int.Parse(item);

                    for (int j = i + 1; j < items.Length; j++)
                    {
                        string item2 = items[j];
                        int val2 = int.Parse(item2);

                        if (IsEvenlyDivisible(val, val2))
                        {
                            hit = true;
                            result = (int)DivideBiggestBySmallest(val, val2);
                            break;
                        }
                    }
                    if (hit) break;
                }

                checksum += result;
            }
            return checksum;
        }

        private static double DivideBiggestBySmallest(int val1, int val2)
        {
            int big = val1,
                small = val2;
            if (val2 > val1)
            {
                big = val2;
                small = val1;
            }
            double div = (double)big / (double)small;
            return div;
        }

        public static bool IsEvenlyDivisible(int val1, int val2)
        {
            double div = DivideBiggestBySmallest(val1, val2);

            return Math.Truncate(div) == div;
        }

        public static int Solve1(string[] numbers)
        {
            int checksum = 0;
            foreach (string line in numbers)
            {
                string[] items = StringUtil.Split(line);

                var min = int.MaxValue;
                var max = int.MinValue;
                foreach (var item in items)
                {
                    int val = int.Parse(item);
                    if (val < min) min = val;
                    if (val > max) max = val;

                }
                int check = max - min;
                checksum += check;
            }
            return checksum;
        }
    }

    public class Day1Captcha
    {
        public static int Solve1(string numbers)
        {
            var result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                int number = int.Parse(numbers[i].ToString());
                int j = i + 1;
                if (j == numbers.Length) j = 0;
                int nextNumber = int.Parse(numbers[j].ToString());

                if (number == nextNumber)
                    result += number;
            }
            return result;
        }

        public static int Solve1Func(string numbers)
        {
            IEnumerable<int> nums = numbers.Select(x => int.Parse(x.ToString())).ToArray();
            return Solve1Func(nums.Last(), nums);
        }

        static int Solve1Func(int num, IEnumerable<int> nums) {

            if (!nums.Any())
                return 0;
           
            return (num == nums.First() ? num : 0) + Solve1Func(nums.First(), nums.Skip(1));
        }

        public static int Solve2(string numbers)
        {
            int length = numbers.Length;
            if (IsEven(length) == false)
                throw new Exception("Only allows for input with even length.");

            int half = length / 2;
            var result = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                int number = int.Parse(numbers[i].ToString());
                int j = i + half;
                if (j >= numbers.Length) j = j - length;
                int nextNumber = int.Parse(numbers[j].ToString());

                if (number == nextNumber)
                    result += number;
            }
            return result;
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }
    }
}
