using System.Collections.Generic;
using System.Linq;

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

    public class Day2
    {
        public class CountResult
        {
            public int TwoCount = 0;
            public int ThreeCount = 0;
        }

        public static int CalculateChecksum(IEnumerable<string> data)
        {
            var totalCount = CalculateTotalCount(data);

            return totalCount.TwoCount * totalCount.ThreeCount;
        }

        public static CountResult CalculateTotalCount(IEnumerable<string> data)
        {
            var totalCount = new CountResult();

            foreach (var row in data)
            {
                var rowResult = CalculateRowCount(row);

                if (rowResult.TwoCount > 0)
                {
                    totalCount.TwoCount++;
                }

                if (rowResult.ThreeCount > 0)
                {
                    totalCount.ThreeCount++;
                }
            }

            return totalCount;
        }

        public static CountResult CalculateRowCount(string row)
        {
            var rowResult = new CountResult();
            var rowCount = CountRow(row);

            foreach (var v in rowCount)
            {
                if (v.Value == 2)
                {
                    rowResult.TwoCount = 1;
                }

                if (v.Value == 3)
                {
                    rowResult.ThreeCount = 1;
                }
            }

            return rowResult;
        }

        public static Dictionary<char, int> CountRow(string row)
        {
            var rowCount = new Dictionary<char, int>();
            foreach (var item in row.ToCharArray())
            {
                if (rowCount.ContainsKey(item))
                {
                    rowCount[item] = rowCount[item] + 1;
                }
                else
                {
                    rowCount.Add(item, 1);
                }
            }

            return rowCount;
        }
    }

    public class Day2Part2
    {
        public static IEnumerable<string> Calculate(IEnumerable<string> data)
        {
            var d = data.ToList();
            int count = data.Count();
            var result = new List<string>();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i+1; j < count; j++)
                {
                    var r = DifferByOne(d[i], d[j]);
                    if (r != null)
                    {
                        result.Add(r);
                    }
                }
            }

            return result;
        }

        public static string DifferByOne(string row1, string row2)
        {
            int differCount = 0;
            int length = row1.Length;
            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                if (row1[i] == row2[i])
                {
                    result = result + row1[i];
                }
                else
                {
                    differCount++;
                }
            }

            if (differCount == 1)
            {
                return result;
            }


            return null;
        }
    }
}
