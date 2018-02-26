

using System.Linq;

namespace AdventOfCode
{
    public class AdventOfCode2017Day9
    {
        public class Stream
        {
            
            private char[] _data;

            public char GetNext()
            {
                if (!_data.Any())
                    return '';
                return _data.Take(1).FirstOrDefault();
            }
        }
    }
}
