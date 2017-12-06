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
    }
}
