using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2._2018
{
    public class AdventOfCode2018Day3
    {
    }

    public class Day3
    {
        public class Size
        {
            public int X;
            public int Y;
        }

        public class Location
        {
            public int X;
            public int Y;
        }

        public class Claim
        {
            public int Id;
            public Size Size;
            public Location Location;
        }

        public class ClaimCount
        {
            public Size Size;

            public int[,] Count;

            public ClaimCount(Size size)
            {
                Size = size;
                Count = new int[size.X, size.Y];
            }
        }

        public class ClaimsResult
        {
            public int OverlappingCount;

            public IEnumerable<int> NonOverlappingClaims;
        }

        public static ClaimsResult Calculate(int boardX, int boardY, IEnumerable<string> claims)
        {
            var boardSize = new Size { X = boardX, Y = boardY };
            var parsedClaims = claims.Select(c => Parse(c));
            return Calculate(boardSize, parsedClaims);
        }

        /// <summary>
        /// Input: #1 @ 1,3: 4x4
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static Claim Parse(string claim)
        {
            claim = claim.Remove(0, 1);
            var split1 = claim.Split('@', StringSplitOptions.RemoveEmptyEntries);
            var split2 = split1[1].Split(':', StringSplitOptions.RemoveEmptyEntries);
            var locationSplit = split2[0].Split(',');
            var sizeSplit = split2[1].Split('x');

            return new Claim
            {
                Id = int.Parse(split1[0]),
                Location = new Location
                {
                    X = int.Parse(locationSplit[0]),
                    Y = int.Parse(locationSplit[1])
                },
                Size = new Size
                {
                    X = int.Parse(sizeSplit[0]),
                    Y = int.Parse(sizeSplit[1])
                }
            };

        }

        public static ClaimsResult Calculate(Size boardSize, IEnumerable<Claim> claims)
        {
            var claimCount = new ClaimCount(boardSize);

            Init(claimCount);

            RunClaims(claimCount, claims);

            return new ClaimsResult()
            {
                OverlappingCount = Count(claimCount), 
                NonOverlappingClaims = FindFreeClaims(claimCount, claims)
            }; 
        }

        public static IEnumerable<int> FindFreeClaims(ClaimCount claimCount, IEnumerable<Claim> claims)
        {
            var freeClaims = new List<int>();
            foreach (var claim in claims)
            {
                var free = IsClaimFree(claimCount, claim);

                if (free)
                {
                    freeClaims.Add(claim.Id);
                }
            }

            return freeClaims;
        }

        public static bool IsClaimFree(ClaimCount claimCount, Claim claim)
        {
            
            for (int i = claim.Location.X; i < claim.Location.X + claim.Size.X; i++)
            {
                for (int j = claim.Location.Y; j < claim.Location.Y + claim.Size.Y; j++)
                {
                    if (claimCount.Count[i, j] > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static int Count(ClaimCount claimCount)
        {
            var count = 0;
            for (int i = 0; i < claimCount.Size.X; i++)
            {
                for (int j = 0; j < claimCount.Size.Y; j++)
                {
                    if (claimCount.Count[i, j] > 1)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static void RunClaims(ClaimCount claimCount, IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                RunClaim(claimCount, claim);
            }
        }

        public static void RunClaim(ClaimCount claimCount, Claim claim)
        {
            for (int i = claim.Location.X; i < claim.Location.X + claim.Size.X; i++)
            {
                for (int j = claim.Location.Y; j < claim.Location.Y + claim.Size.Y; j++)
                {
                    claimCount.Count[i, j] += 1;
                }
            }
        }

        public static void Init(ClaimCount claimCount)
        {
            for (int i = 0; i < claimCount.Size.X; i++)
            {
                for (int j = 0; j < claimCount.Size.Y; j++)
                {
                    claimCount.Count[i, j] = 0;
                }
            }
        }
    }
}
