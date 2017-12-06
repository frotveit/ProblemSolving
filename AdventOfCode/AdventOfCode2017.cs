

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class AdventOfCode2017
    {
    }

    public class Day4PassphraseAnagramChecker
    {
        public static int Solve(string filename)
        {
            string[] lines = FileUtil.GetFile(filename);
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
            string[] lines = FileUtil.GetFile(filename);
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
            List<SpiralMemoryItem> data = new List<SpiralMemoryItem>();

            public void Add(SpiralMemoryState state)
            {
                data.Add(new SpiralMemoryItem(state));
            }
            public void Add(SpiralMemoryItem item)
            {
                data.Add(item);
            }

            public SpiralMemoryItem Get(Pos pos)
            {
                return Get(pos.x, pos.y);
            }

            public SpiralMemoryItem Get(int x, int y)
            {
                return data.FirstOrDefault(d => d.x == x & d.y == y);
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


            public int steps()
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

            public void next()
            {
                value++;
                increaseCurr();
                updateDirection();
                updateSize();
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


            public int steps()
            {
                return Math.Abs(curX) + Math.Abs(curY);
            }
        }
        public static int SolveSteps(int value)
        {
            SpiralMemoryState state = new SpiralMemoryState();
            while (state.value < value)
                state.next();
            return state.steps();
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
