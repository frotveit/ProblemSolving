
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{

    public class AdventOfCode2017Day7
    {
        public class Input
        {
            public string Id;
            public int Weight;
            public IList<string> ChildrenIds;
            public bool Inserted;

        }

        public class Node
        {
            public string Id;
            public int Weight;
            public IList<string> ChildrenIds;

            public IList<Node> Children = new List<Node>();
            public int TotalWeight;

            public int BalancedWidth;
            public int TotalBalancedWidth;

            public Node() { }
            public Node(Input input)
            {
                Id = input.Id;
                Weight = input.Weight;
                ChildrenIds = input.ChildrenIds;
            }
        }

        public class Result
        {
            public Node Root;
            public string RootId;
            public int TotalWeight;
            public int InputTotalWeight;
            public IList<Node> UnbalancedNodes;
            public IList<Node> BalancedNodes;
        }

        public static Result Solve (string filename)
        {
            string[] filedata = FileUtil.GetFile(filename);
            List<Input> inputs = new List<Input>();
            foreach (string row in filedata)
            {
                Input input = ParseRow(row);
                inputs.Add(input);
            }

            Node root = BuildTree(inputs);

            return new Result()
            {
                Root = root,
                RootId = root.Id,
                InputTotalWeight = inputs.Select(x => x.Weight).Sum(),
                TotalWeight = CalculateTotalWeight(root),
                UnbalancedNodes = GetUnbalanced(root),
                BalancedNodes = Balance(root)
            };
        }

        public static List<Node> Balance(Node root, int value = 0)
        {
            if (root.Children.Count == 0)
            {
                if (value == 0)
                    return new List<Node>();
                root.BalancedWidth = value;
                root.Weight += value;
                root.TotalWeight += value;
                return new List<Node> { root };
            }

            if (root.Children.Count == 1)
            {
                if (value == 0)
                {
                    return BalanceChildren(root);
                }
                var balanced = Balance(root.Children.First());
                root.TotalWeight = root.Weight + root.Children.First().TotalWeight;
                return balanced;
            }

            WeightCount wc = new WeightCount(root.Children);
            if (wc.HasUnbalance())
            {
                var node = wc.GetFirstWithCountOne();
                var unbalance = wc.GetUnbalanceValue();
                if (value != 0 && unbalance != value)
                    throw new Exception("Something is wrong");
                var balanced = Balance(node, unbalance);
                root.TotalWeight = root.TotalWeight + node.TotalBalancedWidth + node.BalancedWidth;
                root.TotalBalancedWidth = node.TotalBalancedWidth + node.BalancedWidth;
                balanced.AddRange(BalanceChildren(root));
                return balanced;
            }
            else
            {
                if (value == 0)
                {
                    return BalanceChildren(root);
                }
                root.BalancedWidth = value;
                root.Weight += value;
                root.TotalWeight += value;
                var result = new List<Node> { root };
                result.AddRange(BalanceChildren(root));
                return result;
            }            
        }

        private static List<Node> BalanceChildren(Node root)
        {
            var result = new List<Node>();
            foreach (Node child in root.Children)
            {
                result.AddRange(Balance(child));
            }
            return result;
        }

        public class WeightCount
        {
            public Dictionary<int, List<Node>> Data = new Dictionary<int, List<Node>>();

            public WeightCount() { }
            public WeightCount (IEnumerable<Node> nodes)
            {
                Add(nodes);
            }

            public Node GetFirstWithCountOne()
            {
                if (!Solvable()) throw new Exception("Not solvable.");

                foreach (var nodes in Data.AsEnumerable())
                {
                    if (nodes.Value.Count == 1)
                        return nodes.Value.First();
                }
                return null;
            }

            public bool Solvable()
            {
                return Data.Count == 2 && ((Data[Data.Keys.First()].Count == 1 && Data[Data.Keys.Last()].Count > 1) || (Data[Data.Keys.First()].Count > 1 && Data[Data.Keys.Last()].Count == 1));
            }

            public int GetUnbalanceValue()
            {
                if (!Solvable()) throw new Exception("Not solvable.");

                if (Data[Data.Keys.First()].Count == 1)
                {
                    return Data.Keys.Last() - Data.Keys.First();
                }
                return Data.Keys.First() - Data.Keys.Last();
            }

            public bool HasUnbalance()
            {
                return Data.Count > 1;
            }

            public void Add(IEnumerable<Node> nodes)
            {
                foreach (var n in nodes)
                {
                    Add(n);
                }
            }

            public void Add (Node node)
            {
                if (Data.ContainsKey(node.TotalWeight))
                {
                    Data[node.TotalWeight].Add(node);
                }
                else
                {
                    Data.Add(node.TotalWeight, new List<Node> { node });
                }
            }
        }

        public static List<Node> GetUnbalanced(Node root)
        {
            if (root.Children.Count == 0)
                return new List<Node>();
            if (root.Children.Count == 1)
                return GetUnbalanced(root.Children.First());

            List<Node> unbalanced = new List<Node>();
            int firstChildTotalWeight = root.Children.First().TotalWeight;
            bool unbalance = false;
            foreach(Node n in root.Children)
            {
                unbalanced.AddRange(GetUnbalanced(n));
                if (n.TotalWeight != firstChildTotalWeight)
                    unbalance = true;
            }
            if (unbalance)
                unbalanced.Add(root);
            return unbalanced;
            
        }

        public static int CalculateTotalWeight(Node root)
        {
            root.TotalWeight = root.Weight + root.Children.Select(CalculateTotalWeight).Sum();
            return root.TotalWeight;
        }

        private static Node BuildTree(List<Input> inputs)
        {
            Node root = null;
            while (inputs.Any(x => x.Inserted == false))
                foreach (Input input in inputs.Where(x => x.Inserted == false))
                {
                    Node node = new Node(input);
                    root = Add(root, node, out bool inserted);
                    if (inserted)
                        input.Inserted = true;
                }

            return root;
        }

        public static Node Add(Node root, Node newNode, out bool inserted)
        {
            inserted = false;
            if (root == null)
            {
                root = newNode;
                inserted = true;
            }
            else
            {
                if (newNode.ChildrenIds.Contains(root.Id))  // Ny node peker på rot
                {
                    newNode.Children.Add(root);
                    root = newNode;
                    inserted = true;
                }
                else
                {
                    if (root.ChildrenIds.Contains(newNode.Id)) // Rot peker på ny
                    {
                        root.Children.Add(newNode);
                        inserted = true;
                    }
                    else
                    {
                        foreach (Node node in root.Children)
                        {
                            bool ins = AddBelow(node, newNode);
                            if (ins)
                            {
                                inserted = true;
                                break;
                            }
                        }
                    }
                }
            }
            return root;
        }

        public static bool AddBelow(Node root, Node newNode)
        {

            if (root.ChildrenIds.Contains(newNode.Id)) // Rot peker på ny
            {
                root.Children.Add(newNode);
                return true;
            }
            else
            {
                foreach (Node node in root.Children)
                {
                    if (AddBelow(node, newNode))
                        return true;
                }
            }
            return false;
        }

        public static Input ParseRow(string row)
        {
            string[] splitOn = { " ", "\t", "(", ")", "->", "," };
            string[] rowItems = row.Split(splitOn, options: StringSplitOptions.RemoveEmptyEntries);
            Input input = new Input()
            {
                Id = rowItems[0],
                Weight = int.Parse(rowItems[1]),
                ChildrenIds = rowItems.Skip(2).ToList(),
                Inserted = false
            };
            return input;
        }

        
    }
}
