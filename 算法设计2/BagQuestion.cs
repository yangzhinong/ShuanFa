using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计
{
    public class BagQuestion
    {
        public static void Run()
        {
            var bag = new BagQuestion();
            var n = 5; //n个物品
            var weights = new int[] { 0, 1, 2, 5, 6, 7 };     //物品对应的重量
            var values = new int[] { 0, 1, 6, 18, 22, 28 }; //物品对应的价值
            var maxWeight = 11; //背包可容重量
            var (m, choose) = bag.GetBest(maxWeight, weights, values, n);
            Console.WriteLine("行表示:有多少个物品, 列表示:背包容量, 数值表示:最大价值)");
            Console.Write("      背包可容重量 ");
            for (var w = 1; w <= maxWeight; w++)
            {
                Console.Write(w.ToString().PadLeft(3));
            }
            Console.WriteLine();
            Console.WriteLine("物品序号-价值-重量");
            for (var i = 1; i <= n; i++)
            {
                Console.Write("     ");
                Console.Write(i); Console.Write("---");
                Console.Write(values[i].ToString().PadLeft(2, '0'));
                Console.Write("---");
                Console.Write(weights[i]); Console.Write("    ");
                for (var j = 1; j <= maxWeight; j++)
                {
                    Console.Write(m[i, j].ToString().PadLeft(3));
                }
                Console.WriteLine();
            }

            Console.WriteLine("-------------------------------");

            Console.WriteLine($"所以可装重量{ maxWeight }的背包, 最大可装价值: { m[n, maxWeight]}");
            Console.WriteLine("最优装物品方案 计算表:");
            for (var i = 1; i <= n; i++)
            {
                Console.Write("     ");
                Console.Write(i); Console.Write("---");
                Console.Write(values[i].ToString().PadLeft(2, '0'));
                Console.Write("---");
                Console.Write(weights[i]); Console.Write("    ");
                for (var j = 1; j <= maxWeight; j++)
                {
                    Console.Write(choose[i, j].ToString().PadLeft(3));
                }
                Console.WriteLine();
            }

            Console.Write("所以最优装方案所取的物品号为:");
            bag.PrintChoose(maxWeight, weights, n, choose);
            Console.WriteLine("");
        }

        /// <summary>
        /// 自底向上求解
        /// </summary>
        /// <param name="maxWeight">最大剩余重量</param>
        /// <param name="weights">物品列表,基1编号</param>
        /// <param name="values">价值清单,基1编号</param>
        /// <param name="n">物品数量</param>
        /// <returns>二维数组(行表示:有多少个物品, 列表示:背包容量, 数值表示:最大价值)</returns>
        public (int[,], int[,]) GetBest(int maxWeight, int[] weights, int[] values, int n)
        {
            var opt = new int[n + 1, maxWeight + 1]; // opt[i,w] 表示 有1-i个物品, 背包剩余重w 的最优解值
            var chooses = new int[n + 1, maxWeight + 1];
            for (var i = 1; i <= n; i++) //i表示剩余物品数
            {
                for (var w = 1; w <= maxWeight; w++) //w表示背包可能的还剩的容量
                {
                    if (weights[i] > w) //装不进去
                    {
                        opt[i, w] = opt[i - 1, w];
                    }
                    else
                    {
                        //可以装进去, 则下面两种情况的最大值就是最优解
                        var v1 = opt[i - 1, w];  //不要i号物品, 转为求解 i-1的子问题
                        var v2 = values[i] + opt[i - 1, w - weights[i]]; //要i号物品, 转为求解 i-1的子问题
                        if (v1 > v2)
                        {
                            opt[i, w] = v1;
                        }
                        else
                        {
                            opt[i, w] = v2;
                            chooses[i, w] = 1;
                        }
                    }
                }
            }

            return (opt, chooses);
        }

        /// <summary>
        /// 打印装物品方案
        /// </summary>
        /// <param name="maxWeight"></param>
        /// <param name="weights"></param>
        /// <param name="n"></param>
        /// <param name="chooses"></param>
        public void PrintChoose(int maxWeight, int[] weights, int n, int[,] chooses)
        {
            if (n == 0 && maxWeight <= 0) return;
            if (chooses[n, maxWeight] == 1)
            {
                Console.Write($"{n}号 , ");
                maxWeight -= weights[n];
            }
            else
            {
                n--;
            }
            PrintChoose(maxWeight, weights, n, chooses);
        }
    }
}