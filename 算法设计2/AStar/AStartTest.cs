using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计.AStar
{
    using System;
    using System.Collections.Generic;

    internal class AStartTest
    {
        public static void Test()
        {
            // grid[y][x] = 0: 可通行, 1: 障碍物
            var grid = new List<List<int>>
                    {
                        new() { 0, 1, 1 },
                        new() { 0, 0, 1 },
                        new() { 0, 0, 0 }
                    };

            var start = new Node(0, 0);
            var goal = new Node(2, 2);

            var path = AStarPathfinder.FindPath(grid, start, goal);

            if (path != null)
            {
                foreach (var node in path)
                {
                    Console.WriteLine($"({node.X}, {node.Y})");
                }
            }
            else
            {
                Console.WriteLine("No path found.");
            }
        }
    }
}