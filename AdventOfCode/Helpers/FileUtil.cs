using System.IO;

namespace AdventOfCode.Helpers
{
    public class FileUtil
    {
        public static string[] Get2017File(string filename)
        {
            string[] lines = File.ReadAllLines(@"../../2017/Data/" + filename);
            return lines;
        }
        public static string[] Get2018File(string filename)
        {
            string[] lines = File.ReadAllLines(@"../../2018/Data/" + filename);
            return lines;
        }

        public static string Get2017FileLine(string filename)
        {
            string[] lines = Get2017File(filename);
            if (lines.Length > 0) return lines[0];
            return "";
        }
    }
}
