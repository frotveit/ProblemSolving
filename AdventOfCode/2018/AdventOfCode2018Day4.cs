using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AdventOfCode2._2018.Day4.RadType;

namespace AdventOfCode2._2018
{
    public class AdventOfCode2018Day4
    {
    }

    public class Day4
    {
        public enum RadType
        {
            Udefinert = 0,
            VaktStart = 1,
            PauseStart = 2,
            PauseSlutt = 3
        }

        public class Rad
        {
            public RadType Type;
            public DateTime Tidspunkt;
            public int VaktId;
        }

        public class Pause
        {
            public DateTime Start;
            public DateTime Slutt;
        }

        public class Skift
        {
            public int VaktNr;
            public DateTime Start;
            public DateTime Slutt;

            public IList<Pause> Pauser = new List<Pause>();
        }

        public static void Calculate(IEnumerable<string> data)
        {
            var rader = Parse(data);
            var skift = Process(rader);
        }


        public static IEnumerable<Skift> Process(IEnumerable<Rad> rader)
        {
            var res = new List<Skift>();
            Skift skift = null;
            Pause pause = null;
            foreach (var rad in rader)
            {
                switch (rad.Type)
                {
                    case RadType.VaktStart:
                    {
                        if (skift != null)
                        {
                            skift.Slutt = rad.Tidspunkt.AddMinutes(-1);
                            res.Add(skift);
                        }
                        skift = new Skift() { VaktNr = rad.VaktId, Start = rad.Tidspunkt };
                    }
                        break;
                    case RadType.PauseStart:
                    {
                        pause = new Pause(){Start = rad.Tidspunkt};
                    }
                        break;
                    case RadType.PauseSlutt:
                    {
                        pause.Slutt = rad.Tidspunkt;
                        skift.Pauser.Add(pause);
                    }
                        break;
                }
            }

            skift.Slutt = skift.Start.AddHours(8);  // Avslutt siste skift
            res.Add(skift);
            return res;
        }

        public static IEnumerable<Rad> Parse(IEnumerable<string> data)
        {
            return data.Select(d => Parse(d));
        }

        /// <summary>
        /// Input = "[1518-11-01 00:00] Guard #10 begins shift"
        /// Input = "[1518-11-01 00:05] falls asleep"
        /// Input = "[1518-11-01 00:25] wakes up"
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Rad Parse(string data)
        {
            var tid = new DateTime(
                int.Parse(data.Substring(1,4)), 
                int.Parse(data.Substring(6, 2)),
                int.Parse(data.Substring(9, 2)), 
                int.Parse(data.Substring(12, 2)), 
                int.Parse(data.Substring(15, 2)), 0);

            var typeTegn = data.Substring(19, 1);
            var radType = FinnRadType(typeTegn);

            int id = 0;
            if (radType == RadType.VaktStart)
            {
                id = FinnVaktId(data);
            }
            
            return new Rad
            {
                Tidspunkt = tid,
                Type= radType,
                VaktId = id
            };
        }

        public static int FinnVaktId(string rad)
        {
            return int.Parse(ParseVaktId(rad).Substring(1));
        }

        public static string ParseVaktId(string rad)
        {
            Regex rx = new Regex(@"#[0-9]*");
            var res = rx.Match(rad);
            return res.Value;
        }

        public static RadType FinnRadType(string typeTegn)
        {
            RadType radType = Udefinert;
            switch (typeTegn)
            {
                case "G":
                    radType = VaktStart;
                    break;
                case "f":
                    radType = PauseStart;
                    break;
                case "w":
                    radType = PauseSlutt;
                    break;
            }

            return radType;
        }
    }
}
