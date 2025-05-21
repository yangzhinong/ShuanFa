namespace 算法设计.AStar
{
    using System;
    using System.Collections.Generic;

    public class Node : IComparable<Node>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int GCost { get; set; } // Cost from start to this node
        public int HCost { get; private set; } // Heuristic cost from this node to goal
        public int FCost { get => GCost + HCost; }
        public Node Parent { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void CalculateHCost(Node goal)
        {
            // Using Manhattan distance as the heuristic
            HCost = Math.Abs(goal.X - X) + Math.Abs(goal.Y - Y);
        }

        public IEnumerable<Node> GetNeighbors(List<List<int>> grid)
        {
            var neighbors = new List<Node>();
            int rows = grid.Count;
            int cols = grid[0].Count;
            // 通行的邻居节点, 可以调整顺序以改变搜索策略
            // 上
            if (Y > 0 && grid[Y - 1][X] == 0)
                neighbors.Add(new Node(X, Y - 1));
            // 下
            if (Y < rows - 1 && grid[Y + 1][X] == 0)
                neighbors.Add(new Node(X, Y + 1));
            // 左
            if (X > 0 && grid[Y][X - 1] == 0)
                neighbors.Add(new Node(X - 1, Y));
            // 右
            if (X < cols - 1 && grid[Y][X + 1] == 0)
                neighbors.Add(new Node(X + 1, Y));

            return neighbors;
        }

        public int CompareTo(Node? other)
        {
            if (other is null)
            {
                return 1; // Current node is considered greater if other is null
            }

            int fCostComparison = FCost.CompareTo(other.FCost);
            if (fCostComparison == 0)
            {
                // If FCost is the same, compare HCost to break ties
                return HCost.CompareTo(other.HCost);
            }
            return fCostComparison;
        }
    }
}