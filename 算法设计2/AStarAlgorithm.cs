using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计
{
    /// <summary>
    /// 提供A*算法用于计算从源点到所有节点的最短路径（可选路径还原）。
    /// </summary>
    /// <remarks>
    /// 适用于有非负权重的图。需要用户提供启发式函数（如欧几里得距离、曼哈顿距离等）。
    /// </remarks>
    public class AStarAlgorithm
    {
        /// <summary>
        /// 计算从源点到目标节点的最短路径及路径还原。
        /// </summary>
        /// <param name="graph">图的邻接表表示。key为节点，value为相邻节点及其权重。</param>
        /// <param name="source">源点。</param>
        /// <param name="target">目标节点。</param>
        /// <param name="heuristic">启发式函数，参数为当前节点和目标节点，返回估算距离。</param>
        /// <returns>
        /// Tuple:
        ///   Item1: 目标节点的最短距离（不可达为int.MaxValue）。
        ///   Item2: 前驱节点字典（可用于路径还原）。
        /// </returns>
        public static (int distance, Dictionary<int, int> previous) FindShortestPath(
            Dictionary<int, List<(int neighbor, int weight)>> graph,
            int source,
            int target,
            Func<int, int, int> heuristic)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, int>();
            var openSet = new PriorityQueue<int, int>();
            var closedSet = new HashSet<int>();

            foreach (var node in graph.Keys)
                distances[node] = int.MaxValue;
            distances[source] = 0;
            openSet.Enqueue(source, heuristic(source, target));

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();
                if (current == target)
                    break;
                if (closedSet.Contains(current))
                    continue;
                closedSet.Add(current);

                if (!graph.TryGetValue(current, out var neighbors))
                    continue;

                foreach (var (neighbor, weight) in neighbors)
                {
                    int tentativeG = distances[current] + weight;
                    if (tentativeG < distances.GetValueOrDefault(neighbor, int.MaxValue))
                    {
                        distances[neighbor] = tentativeG;
                        previous[neighbor] = current;
                        int f = tentativeG + heuristic(neighbor, target);
                        openSet.Enqueue(neighbor, f);
                    }
                }
            }

            return (distances.GetValueOrDefault(target, int.MaxValue), previous);
        }

        /// <summary>
        /// 还原从源点到目标节点的最短路径。
        /// </summary>
        /// <param name="previous">前驱节点字典。</param>
        /// <param name="source">源点。</param>
        /// <param name="target">目标节点。</param>
        /// <returns>最短路径上的节点列表（从源点到目标节点）。如果不可达，返回空列表。</returns>
        public static List<int> ReconstructPath(Dictionary<int, int> previous, int source, int target)
        {
            var path = new List<int>();
            int current = target;
            while (current != source)
            {
                path.Add(current);
                if (!previous.TryGetValue(current, out current))
                    return new List<int>();
            }
            path.Add(source);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// 示例测试
        /// </summary>
        public static void Test()
        {
            var graph = new Dictionary<int, List<(int neighbor, int weight)>>()
            {
                { 1, new List<(int, int)>{ (2, 10), (6, 3) } },
                { 2, new List<(int, int)>{ (4, 5), (3, 7) } },
                { 3, new List<(int, int)>{  } },
                { 4, new List<(int, int)>{ (3, 4), (5, 7) } },
                { 5, new List<(int, int)>{ } },
                { 6, new List<(int, int)>{ (4, 6), (5, 1),(2,2) } },
            };

            // 这里的启发式函数为0，等价于Dijkstra
            Func<int, int, int> heuristic = (a, b) => 0;

            int source = 1, target = 5;
            var (distance, previous) = FindShortestPath(graph, source, target, heuristic);
            Console.WriteLine($"From {source} to {target}: Distance = {distance}");
            Console.WriteLine("Path: " + string.Join("->", ReconstructPath(previous, source, target)));
        }
    }
}
