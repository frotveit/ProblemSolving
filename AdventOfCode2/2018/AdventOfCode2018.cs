using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2._2018
{
    public class AdventOfCode2018
    {
    }

    public class Day1
    {
        public static int CalculateFrequency(int start, IEnumerable<string> data)
        {
            var d = data.Select(GetValue);

            return CalculateFrequency(start, d);
        }

        public static int CalculateFrequency(int start, IEnumerable<int> data)
        {
            var result = start;
            foreach (var value in data)
            {
                result += value;
            }

            return result;
        }

        public static int GetValue(string item)
        {
            var sign = item.Substring(0, 1);
            var val = item.Substring(1);
            var value = int.Parse(val);
            if (sign == "-")
            {
                value = -value;
            }

            return value;
        }

        public static int CalculateRecurringFrequency(int start, IEnumerable<string> data)
        {
            var d = data.Select(GetValue);

            return CalculateRecurringFrequency(start, d);
        }

        public static int CalculateRecurringFrequency(int start, IEnumerable<int> data)
        {
            var result = start;
            HashSet<int> values = new HashSet<int>();
            values.Add(start);

            while (true)
            {
                foreach (var value in data)
                {
                    result += value;
                    if (values.Contains(result))
                    {
                        return result;
                    }

                    values.Add(result);
                }
            }
        }
    }
}
