using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Input = AdventOfCode._2017.AdventOfCode2017Day7.Input;
using Node = AdventOfCode._2017.AdventOfCode2017Day7.Node;

namespace AdventOfCode._2017.Test
{
    [TestClass]
    public class AdventOfCode2017Day7Test
    {
        [TestMethod]
        public void Test_Day7()
        {
            var result1 = AdventOfCode2017Day7.Solve("Day7Data.txt");
            Assert.AreEqual("tknk", result1.RootId);
            Assert.AreEqual(778, result1.InputTotalWeight);
            Assert.AreEqual(778, result1.TotalWeight);
            Assert.AreEqual(1, result1.BalancedNodes.Count);
            var node1 = result1.BalancedNodes.First();
            Assert.AreEqual(-8, node1.BalancedWidth);
            Assert.AreEqual(60, node1.Weight);
            Assert.AreEqual("ugml", node1.Id);


            var result2 = AdventOfCode2017Day7.Solve("Day7FullData.txt");
            Assert.AreEqual("gynfwly", result2.RootId);
            Assert.AreEqual(1, result2.BalancedNodes.Count);
            var node2 = result2.BalancedNodes.First();            
            Assert.AreEqual(1526, node2.Weight);            
        }

        [TestMethod]
        public void Test_ParseRow()
        {
            Input res = AdventOfCode2017Day7.ParseRow("aa (19) -> bb, cc, dd");
            Assert.AreEqual("aa", res.Id);
            Assert.AreEqual(19, res.Weight);
            Assert.AreEqual(3, res.ChildrenIds.Count);
            Assert.IsTrue(res.ChildrenIds.Contains("bb"));
            Assert.IsTrue(res.ChildrenIds.Contains("cc"));
            Assert.IsTrue(res.ChildrenIds.Contains("dd"));

            res = AdventOfCode2017Day7.ParseRow("aa (19) -> bb");
            Assert.AreEqual("aa", res.Id);
            Assert.AreEqual(19, res.Weight);
            Assert.AreEqual(1, res.ChildrenIds.Count);
            Assert.IsTrue(res.ChildrenIds.Contains("bb"));

            res = AdventOfCode2017Day7.ParseRow("aa (19)");
            Assert.AreEqual("aa", res.Id);
            Assert.AreEqual(19, res.Weight);
            Assert.AreEqual(0, res.ChildrenIds.Count);            
        }

        [TestMethod]
        public void Test_CalculateTotalWeight()
        {
            Node root = new Node { Weight = 3 };
            Assert.AreEqual(3, AdventOfCode2017Day7.CalculateTotalWeight(root));

            root.Children.Add(new Node { Weight = 2 });
            Assert.AreEqual(5, AdventOfCode2017Day7.CalculateTotalWeight(root));

            root.Children.Add(new Node { Weight = 7 });
            Assert.AreEqual(12, AdventOfCode2017Day7.CalculateTotalWeight(root));

            root.Children[0].Children.Add(new Node { Weight = 1 });
            Assert.AreEqual(13, AdventOfCode2017Day7.CalculateTotalWeight(root));
        }

        [TestMethod]
        public void Test_CalculateTotalWeightMultipleLevels()
        {
            Node root = new Node { Weight = 1, Id = "A" };

            root.Children.Add(new Node { Weight = 13, Id = "AA" });
            root.Children.Add(new Node { Weight = 13, Id = "AB" });
            Node ac = new Node { Weight = 1, Id = "AC" };
            root.Children.Add(ac);

            ac.Children.Add(new Node { Weight = 4, Id = "ACA" });
            ac.Children.Add(new Node { Weight = 4, Id = "ACB" });
            Node acc = new Node { Weight = 0, Id = "ACC" };
            ac.Children.Add(acc);

            acc.Children.Add(new Node { Weight = 1, Id = "ACCA" });
            acc.Children.Add(new Node { Weight = 1, Id = "ACCB" });
            acc.Children.Add(new Node { Weight = 1, Id = "ACCC" });

            var result = AdventOfCode2017Day7.CalculateTotalWeight(root);
            
            Assert.AreEqual(3, acc.TotalWeight);
            Assert.AreEqual(12, ac.TotalWeight);
            Assert.AreEqual(39, root.TotalWeight);
            Assert.AreEqual(39, result);
        }

        [TestMethod]
        public void Test_BalanceSingleNode()
        {
            Node root = new Node { Weight = 1, TotalWeight = 1 };
            var result = AdventOfCode2017Day7.Balance(root);

            Assert.AreEqual(0, result.Count);

            result = AdventOfCode2017Day7.Balance(root, 2);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result.First().BalancedWidth);
            Assert.AreEqual(3, result.First().Weight);
            Assert.AreEqual(3, result.First().TotalWeight);           
        }

        [TestMethod]
        public void Test_BalanceOneLevel()
        {
            Node root = new Node { Weight = 1, TotalWeight = 3, Id = "A" };
            root.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "AA" });
            root.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "AB" });
            root.Children.Add(new Node { Weight = 0, TotalWeight = 0, Id = "AC" });

            var result = AdventOfCode2017Day7.Balance(root);            

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("AC", result.First().Id);
            Assert.AreEqual(1, result.First().BalancedWidth);
            Assert.AreEqual(1, result.First().Weight);
            Assert.AreEqual(1, result.First().TotalWeight);

            Assert.AreEqual(4, root.TotalWeight);
        }

        [TestMethod]
        public void Test_BalanceTwoLevels()
        {
            Node root = new Node { Weight = 1, TotalWeight = 12, Id = "A" };
            root.Children.Add(new Node { Weight = 4, TotalWeight = 4, Id = "AA" });
            root.Children.Add(new Node { Weight = 4, TotalWeight = 4, Id = "AB" });
            Node ac = new Node { Weight = 1, TotalWeight = 3, Id = "AC" };
            root.Children.Add(ac);

            ac.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "ACA" });
            ac.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "ACB" });
            ac.Children.Add(new Node { Weight = 0, TotalWeight = 0, Id = "ACC" });

            var result = AdventOfCode2017Day7.Balance(root);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ACC", result.First().Id);
            Assert.AreEqual(1, result.First().BalancedWidth);
            Assert.AreEqual(1, result.First().Weight);
            Assert.AreEqual(1, result.First().TotalWeight);

            Assert.AreEqual(4, ac.TotalWeight);
            Assert.AreEqual(13, root.TotalWeight);
        }

        [TestMethod]
        public void Test_BalanceSecondOfThreeLevels()
        {
            Node root = new Node { Weight = 1, TotalWeight = 39, Id = "A" };

            root.Children.Add(new Node { Weight = 13, TotalWeight = 13, Id = "AA" });
            root.Children.Add(new Node { Weight = 13, TotalWeight = 13, Id = "AB" });
            Node ac = new Node { Weight = 1, TotalWeight = 12, Id = "AC" };
            root.Children.Add(ac);

            ac.Children.Add(new Node { Weight = 4, TotalWeight = 4, Id = "ACA" });
            ac.Children.Add(new Node { Weight = 4, TotalWeight = 4, Id = "ACB" });
            Node acc = new Node { Weight = 0, TotalWeight = 3, Id = "ACC" };
            ac.Children.Add(acc);

            acc.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "ACCA" });
            acc.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "ACCB" });            
            acc.Children.Add(new Node { Weight = 1, TotalWeight = 1, Id = "ACCC" });

            var result = AdventOfCode2017Day7.Balance(root);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ACC", result.First().Id);
            Assert.AreEqual(1, result.First().BalancedWidth);
            Assert.AreEqual(1, result.First().Weight);
            Assert.AreEqual(4, result.First().TotalWeight);

            Assert.AreEqual(13, ac.TotalWeight);
            Assert.AreEqual(40, root.TotalWeight);
        }

    }
}
