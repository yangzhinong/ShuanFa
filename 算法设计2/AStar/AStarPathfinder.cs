namespace 算法设计.AStar
{
    using System.Collections.Generic;

    public class AStarPathfinder
    {
        public static List<Node> FindPath(List<List<int>> grid, Node start, Node goal)
        {
            var openSet = new SortedSet<Node>();
            var closedSet = new HashSet<(int, int)>();

            start.GCost = 0;
            start.CalculateHCost(goal);
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                var currentNode = openSet.Min;
                openSet.Remove(currentNode);

                // 检查是否到达目标节点
                if (currentNode.X == goal.X && currentNode.Y == goal.Y)
                {
                    return ReconstructPath(start, currentNode);
                }

                closedSet.Add((currentNode.X, currentNode.Y));

                foreach (var neighbor in currentNode.GetNeighbors(grid))
                {
                    if (closedSet.Contains((neighbor.X, neighbor.Y)))
                        continue;

                    // 检查邻居节点是否在openSet中
                    int tentativeGCost = currentNode.GCost + 1;
                    neighbor.GCost = tentativeGCost;
                    neighbor.CalculateHCost(goal);
                    neighbor.Parent = currentNode;

                    // 检查openSet中是否已有更优的节点
                    var existing = openSet.TryGetValue(neighbor, out var existingNode) ? existingNode : null;
                    if (existingNode != null && tentativeGCost >= existingNode.GCost)
                        continue;

                    openSet.Remove(neighbor); // 移除旧的
                    openSet.Add(neighbor);    // 添加新的
                }
            }

            return null; // 未找到路径
        }

        private static List<Node> ReconstructPath(Node start, Node goal)
        {
            var path = new List<Node>();
            var current = goal;
            while (current != null && (current.X != start.X || current.Y != start.Y))
            {
                path.Add(current);
                current = current.Parent;
            }
            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}