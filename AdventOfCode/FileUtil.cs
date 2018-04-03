using System.IO;

namespace AdventOfCode
{
    public class FileUtil
    {
        public static string[] GetFile(string filename)
        {
            string[] lines = File.ReadAllLines(@"../../Data/" + filename);
            return lines;
        }

        public static string GetFileLine(string filename)
        {
            string[] lines = GetFile(filename);
            if (lines.Length > 0) return lines[0];
            return "";
        }
    }
}
