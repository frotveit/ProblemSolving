using System.IO;

namespace AdventOfCode.Helpers
{
    public static class FileUtil
    {
        public static string[] GetFile(string relLocation, string filename)
        {
            string[] lines = File.ReadAllLines($"../../../{relLocation}/{filename}");
            return lines;
        }

        public static string[] Get2017File(string filename)
        {
            string[] lines = File.ReadAllLines(@"../../../2017/Data/" + filename);
            return lines;
        }

        public static string Get2017FileLine(string filename)
        {
            string[] lines = Get2017File(filename);
            if (lines.Length > 0) return lines[0];
            return "";
        }

        public static string[] Get2018File(string filename)
        {
            string[] lines = File.ReadAllLines(@"../../../2018/Data/" + filename);
            return lines;
        }

        public static string Get2018FileLine(string filename)
        {
            string[] lines = Get2018File(filename);
            if (lines.Length > 0) return lines[0];
            return "";
        }        
    }
}
