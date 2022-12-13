using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day12Problem : Problem
    {
        protected override string InputName => "Actual";

        private string[] Input { get; init; }

        public Day12Problem()
        {
            Input = GetInputValue();

        }

        public override object PartOne()
        {
            var (nodes, startNode, endNode) = GetNodes(Input);

            var path = GetShortestPath(nodes, startNode, endNode);

            return path.Last().PathDistanceTraveled;
        }

        public override object PartTwo()
        {
            var (nodes, _, endNode) = GetNodes(Input);

            var startNodeLocations = nodes
                .Where(n => n.Value.Height == 'a')
                .ToList()
                .Select(x => x.Key);

            var shortestPath = 2000;

            foreach (var startNodeLocation in startNodeLocations)
            {
                var startNode = nodes[startNodeLocation];
                var path = GetShortestPath(nodes, startNode, endNode);

                var distance = path.LastOrDefault()?.PathDistanceTraveled ?? 2000;
                if (distance < shortestPath)
                {
                    shortestPath = distance;
                }

                // Reset the nodes path distance and parents
                nodes.ForEach(x => x.Value.ResetNode());
            }

            return shortestPath;
        }

        private static List<Node> GetShortestPath(Dictionary<(int x, int y), Node> nodes, Node startNode, Node endNode)
        {
            var queue = new List<Node>() { startNode };
            var visited = new Dictionary<(int x, int y), Node>();

            while (queue.Any())
            {
                var checkNode = queue.OrderBy(x => x.PathDistanceTraveled).First();

                if (checkNode == endNode)
                {
                    var path = new List<Node>();

                    var currentNode = endNode;

                    while (currentNode.ParentNode != null)
                    {
                        path.Add(currentNode);
                        currentNode = currentNode.ParentNode;
                    }

                    path.Reverse();

                    //Console.WriteLine($"Checked {visited.Count} out of {nodes.Count} nodes");

                    //PrintPath(nodes, path, visited);

                    return path;
                }

                visited[(checkNode.X, checkNode.Y)] = checkNode;
                queue.Remove(checkNode);

                foreach (var node in checkNode.AdjacentNodes)
                {
                    if (visited.ContainsKey((node.X, node.Y)))
                    {
                        // Already checked this node. No need to look again. 
                        continue;
                    }

                    if (queue.Contains(node))
                    {
                        node.TryAddNewParent(checkNode);
                    }
                    else
                    {
                        node.UpdateParentNode(checkNode);
                        queue.Add(node);
                    }
                }
            }

            // Shouldn't ever reach here. 
            return new List<Node>();
        }

        private static (Dictionary<(int x, int y), Node> nodes, Node startNode, Node endNode) GetNodes(string[] input)
        {
            Node startNode = null;
            Node endNode = null;
            var nodes = new Dictionary<(int x, int y), Node>();

            var height = input.Length;
            var width = input[0].Length;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var nodeHeight = input[y][x];
                    var newNode = new Node(x, y, input[y][x]);
                    if (nodeHeight == 'S')
                    {
                        startNode = newNode;
                        newNode.Height = 'a';
                    }
                    if (nodeHeight == 'E')
                    {
                        endNode = newNode;
                        newNode.Height = 'z';
                    }
                    nodes[(x, y)] = newNode;
                }
            }

            return (FillAdjacentNodes(nodes, height, width), startNode, endNode);
        }

        private static void PrintPath(Dictionary<(int x, int y), Node> allNodes, List<Node> path, Dictionary<(int x, int y), Node> visited)
        {
            var height = allNodes.Keys.Max(k => k.y);
            var width = allNodes.Keys.Max(k => k.x);

            Console.WriteLine("");
            for (int y = 0; y < height + 1; y++)
            {
                Console.WriteLine("");
                for (int x = 0; x < width + 1; x++)
                {
                    if (path.Any(n => n.X == x && n.Y == y))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(allNodes[(x, y)].Height);

                    }
                    else if (visited.Any(n => n.Value.X == x && n.Value.Y == y))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(allNodes[(x, y)].Height);
                    }
                    else
                    {
                        Console.Write(allNodes[(x, y)].Height);
                        //Console.Write(".");
                    }
                    Console.ResetColor();
                }
            }
            Console.WriteLine("");
        }

        private static Dictionary<(int X, int Y), Node> FillAdjacentNodes(Dictionary<(int X, int Y), Node> nodes, int height, int width)
        {
            foreach (var dn in nodes)
            {
                var node = dn.Value;
                if (node.X > 0)
                {
                    // Left
                    node.AddAdjacentNode(nodes[(node.X - 1, node.Y)]);
                }
                if (node.X < width - 1)
                {
                    // Right
                    node.AddAdjacentNode(nodes[(node.X + 1, node.Y)]);
                }
                if (node.Y > 0)
                {
                    // Top
                    node.AddAdjacentNode(nodes[(node.X, node.Y - 1)]);
                }
                if (node.Y < height - 1)
                {
                    // Bottom
                    node.AddAdjacentNode(nodes[(node.X, node.Y + 1)]);
                }
            }

            return nodes;
        }
    }

    internal class Node
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char Height { get; set; }

        public int PathDistanceTraveled { get; private set; }

        public List<Node> AdjacentNodes { get; private set; }

        public Node? ParentNode { get; private set; }

        public Node(int x, int y, char height)
        {
            X = x;
            Y = y;
            Height = height;
            AdjacentNodes = new List<Node>();
        }

        public void AddAdjacentNode(Node adjacentNode)
        {
            if (adjacentNode.Height - Height <= 1)
            {
                AdjacentNodes.Add(adjacentNode);
            }
        }

        public void UpdateParentNode(Node parent)
        {
            ParentNode = parent;
            PathDistanceTraveled = parent.PathDistanceTraveled + 1;
        }

        public void TryAddNewParent(Node newParent)
        {
            var potentialNewDistance = newParent.PathDistanceTraveled + 1;

            if (potentialNewDistance < PathDistanceTraveled)
            {
                UpdateParentNode(newParent);
            }
        }

        public void ResetNode()
        {
            PathDistanceTraveled = 0;
            ParentNode = null;
        }
    }
}
