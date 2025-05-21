using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计
{
    /// <summary>
    /// Provides methods to compute the shortest paths from a source node to all other nodes in a graph using Dijkstra's
    /// algorithm.
    /// </summary>
    /// <remarks>Dijkstra's algorithm is a graph search algorithm that solves the single-source shortest path
    /// problem for a graph with non-negative edge weights. This class is designed to work with directed or undirected
    /// graphs and assumes that the graph is represented in a suitable format (e.g., adjacency list or
    /// matrix).</remarks>
    public class DijkstraAlgorithm
    {
        /// <summary>
        /// 计算从源点到所有节点的最短路径。
        /// </summary>
        /// <param name="graph">图的邻接表表示。key为节点，value为相邻节点及其权重。</param>
        /// <param name="source">源点。</param>
        /// <returns>每个节点的最短距离字典。</returns>
        public static Dictionary<int, int> ComputeShortestPathsOnlyReturnDistance(Dictionary<int, List<(int neighbor, int weight)>> graph, int source)
        {
            var distances = new Dictionary<int, int>();
            var visited = new HashSet<int>();
            var priorityQueue = new PriorityQueue<int, int>();

            // 初始化距离
            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;
            }
            distances[source] = 0;
            priorityQueue.Enqueue(source, 0);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);

                if (!graph.TryGetValue(current, out var neighbors))
                    continue;

                foreach (var (neighbor, weight) in neighbors)
                {
                    if (distances[current] + weight < distances.GetValueOrDefault(neighbor, int.MaxValue))
                    {
                        distances[neighbor] = distances[current] + weight;
                        priorityQueue.Enqueue(neighbor, distances[neighbor]);
                    }
                }
            }

            return distances;
        }

        /// <summary>
        /// 计算从源点到所有节点的最短路径及路径还原。
        /// </summary>
        /// <param name="graph">图的邻接表表示。key为节点，value为相邻节点及其权重。</param>
        /// <param name="source">源点。</param>
        /// <returns>
        /// Tuple:
        ///   Item1: 每个节点的最短距离字典。
        ///   Item2: 每个节点的前驱节点字典（可用于路径还原）。
        /// </returns>
        public static (Dictionary<int, int> distances, Dictionary<int, int> previous) ComputeShortestPaths(
            Dictionary<int, List<(int neighbor, int weight)>> graph, int source)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, int>();
            var visited = new HashSet<int>();
            var priorityQueue = new PriorityQueue<int, int>();

            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;
            }
            distances[source] = 0;
            priorityQueue.Enqueue(source, 0);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);

                if (!graph.TryGetValue(current, out var neighbors))
                    continue;

                foreach (var (neighbor, weight) in neighbors)
                {
                    int newDist = distances[current] + weight;
                    if (newDist < distances.GetValueOrDefault(neighbor, int.MaxValue))
                    {
                        distances[neighbor] = newDist;
                        previous[neighbor] = current;
                        priorityQueue.Enqueue(neighbor, newDist);
                    }
                }
            }

            return (distances, previous);
        }

        /// <summary>
        /// 还原从源点到目标节点的最短路径。
        /// </summary>
        /// <param name="previous">前驱节点字典。</param>
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
                {
                    // 不可达
                    return new List<int>();
                }
            }
            path.Add(source);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// 测试
        /// https://zhuanlan.zhihu.com/p/454373256
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

            var (distance, previous) = ComputeShortestPaths(graph, 1);
            foreach (var key in distance.Keys)
            {
                Console.WriteLine($"Node {key}: Distance {distance[key]}");

                Console.WriteLine(string.Join("->", ReconstructPath(previous, 1, key)));

                Console.WriteLine("=======================");
            }
        }
    }
}
