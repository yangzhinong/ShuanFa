using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计2
{
    public class BagQuestion
    {
        /// <summary>
        /// 自底向上求解
        /// </summary>
        /// <param name="maxWeight">最大剩余重量</param>
        /// <param name="weights">物品列表,基1编号</param>
        /// <param name="values">价值清单,基1编号</param>
        /// <param name="n">物品数量</param>
        /// <returns>二维数组(行表示:有多少个物品, 列表示:背包容量, 数值表示:最大价值)</returns>
        public int[,] GetBest(int maxWeight, int[] weights, int[] values, int n)
        {
            var opt = new int[n + 1, maxWeight + 1]; // opt[i,w] 表示 有1-i个物品, 背包剩余重w 的最优解值
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
                        opt[i, w] = Math.Max(
                                    opt[i - 1, w],  //不要i号物品, 转为求解 i-1的子问题
                                    values[i] + opt[i - 1, w - weights[i]] //要i号物品, 转为求解 i-1的子问题
                                        );
                    }
                }
            }
            return opt;
        }
    }

    /// <summary>
    /// 背包问题解
    /// </summary>
    public class BagAnswer
    {
        /// <summary>
        /// 物品数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 背重可容重量
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 物品数-可容重 -对应的- 最大价值
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 物品序号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Selection { get; set; }
    }

    /// <summary>
    /// 背包
    /// </summary>
    public class Bag
    {
        /// <summary>
        /// 背包最大可装多重的东西
        /// </summary>
        public int Capacity { get; set; }
    }

    /// <summary>
    /// 物品
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 物品编号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 价值
        /// </summary>
        public int Value { get; set; }
    }
}