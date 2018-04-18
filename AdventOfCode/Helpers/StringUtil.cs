

using System;

namespace AdventOfCode
{
    public class StringUtil
    {
        public static string[] Split(string line)
        {
            string[] splitOn = { " ", "\t" };
            string[] words = line.Split(splitOn, options: StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        public static string[] SplitOnComma(string line)
        {
            string[] splitOn = { "," };
            string[] words = line.Split(splitOn, options: StringSplitOptions.RemoveEmptyEntries);
            return words;
        }
    }
}
