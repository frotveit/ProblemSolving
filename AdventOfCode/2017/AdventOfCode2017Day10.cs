using System;
using System.Collections.Generic;

namespace AdventOfCode._2017
{
    public class AdventOfCode2017Day10
    {
        public static int Solve(int size, List<int> input)
        {
            var numbers = new NumberList(size);

            foreach (var length in input)
            {
                if (length <= size)
                {
                    numbers.Process(length);
                }
            }

            return numbers.Numbers[0] * numbers.Numbers[1];
        }

        public static string Solve2(string input)
        {
            int size = 256;
            var numbers = new NumberList(size);
            List<int> inputNumbers = ConvertToAscii(input);
            inputNumbers.AddRange(new List<int> {17, 31, 73, 47, 23});

            for (int i = 1; i <= 64; i++)
            {
                foreach (var length in inputNumbers)
                {
                    if (length <= size)
                    {
                        numbers.Process(length);
                    }
                }
            }

            var hash = MakeDenseHash(numbers.Numbers);
            return FormatHash(hash);
        }

        public static string FormatHash(List<int> hash)
        {
            string s = "";
            foreach (var number in hash)
            {
                string formatted = number.ToString("X").ToLower();
                if (formatted.Length == 1) formatted = "0" + formatted;
                s = s + formatted;
            }

            return s;
        }

        public static List<int> MakeDenseHash(List<int> numbers)
        {
            List<List<int>> split = SplitForHash(numbers);
            List<int> hash = new List<int>();
            foreach (var list in split)
            {
                hash.Add(Hash(list));
            }

            return hash;
        }

        public static int Hash(List<int> numbers)
        {
            if (numbers.Count != 16)
                throw new Exception("Skal være 16 elementer.");
            int result = 0;
            foreach (int number in numbers)
                result = result ^ number;
            return result;
        }

        public static List<List<int>> SplitForHash(List<int> numbers)
        {
            List<List<int>> split = new List<List<int>>();
            for (int i = 0; i < numbers.Count; i += 16)
            {
                split.Add(numbers.GetRange(i, 16));
            }
            return split;
        } 

        public static List<int> ConvertToAscii(string input)
        {
            List<int> output = new List<int>();
            foreach (char v in input)
            {
                output.Add((int) v);
            }

            return output;
        }

        public class NumberList
        {
            public readonly int Size;
            public List<int>  Numbers = new List<int>();
            public int Start = 0;
            public int Skip = 0;

            public NumberList(int size)
            {
                Size = size;
                for (int i = 0; i < size; i++)
                {
                    Numbers.Add(i);
                }
            }

            public List<int> Get(int length)
            {
                var result = new List<int>();
                int pos = Start;
                for (int i = 0; i < length; i++)
                {
                    if (pos >= Size) pos = pos - Size;
                    result.Add(Numbers[pos]);
                    pos++;
                }

                return result;
            }

            public void Replace(int length, List<int> newNumbers)
            {
                int pos = Start;
                for (int i = 0; i < length; i++)
                {
                    if (pos == Size) pos = 0;
                    Numbers[pos] = newNumbers[i];
                    pos++;
                }
            }

            public void Next(int length)
            {
                int start = Start + length + Skip;
                while (start >= Size) start = start - Size;
                Start = start;
                Skip++;
            }

            public void Process(int length)
            {
                var list = Get(length);
                list.Reverse();
                Replace(length, list);
                Next(length);
            }

        }
    }
}
