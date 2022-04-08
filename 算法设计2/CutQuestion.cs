using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计
{
    /// <summary>
    /// 钢管切割问题
    /// </summary>
    public class CutQuestion
    {
        public static void Run()
        {
            var question = new CutQuestion();
            var memo = new MemoDic();
            var length = 15;
            var max = question.Cut(length, new List<LengthValueTable>()
            {
                 new LengthValueTable(1,1),
                 new LengthValueTable(2,5),
                 new LengthValueTable(3,8),
                 new LengthValueTable(4,9),
                 new LengthValueTable(5,10),
                 new LengthValueTable(6,17),
                 new LengthValueTable(7,17),
                 new LengthValueTable(8,20),
                 new LengthValueTable(9,24),
                 new LengthValueTable(10,30),
            }, memo);

            Console.WriteLine($"{length}米的钢管,能获得的最大价值:{max}");
            Console.Write("切割的方法是:");
            question.PrintCutMethod(memo, length);

            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// 打印切割方法
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="length"></param>
        public void PrintCutMethod(MemoDic memo, int length)
        {
            if (length <= 0) return;
            if (memo.DicLengthChoose.ContainsKey(length))
            {
                var cutLen = memo.DicLengthChoose[length];
                if (cutLen <= 0) return;
                Console.Write(cutLen);
                Console.Write("米  ");
                PrintCutMethod(memo, length - cutLen);
            }
        }

        /// <summary>
        /// 求解某个长度的钢管,按长度价值表,找出最大的价值以及切法
        /// </summary>
        /// <param name="totalLength">钢管总长度</param>
        /// <param name="tables">长度价格表</param>
        /// <param name="memos">求解过程备忘录</param>
        /// <returns>最大价值</returns>
        public int Cut(int totalLength, List<LengthValueTable> tables, MemoDic memos)
        {
            //先从备忘录中查是否已经计算过
            if (memos.DicLengthValue.ContainsKey(totalLength)) return memos.DicLengthValue[totalLength];

            int max = 0;
            int cutLen = 0;
            foreach (var item in tables)
            {
                if (totalLength >= item.Length) //这个条件加上上面的for循环就当于枚举所有情况的最大值
                {
                    var tempValue = (item.Value + Cut(totalLength - item.Length, tables, memos));
                    if (tempValue > max)
                    {
                        max = tempValue;   //记录最优解
                        cutLen = item.Length; //记录下最优时切下的长度
                    }
                }
            }
            //记录下这个长度的最大价值
            memos.DicLengthValue.Add(totalLength, max);
            memos.DicLengthChoose.Add(totalLength, cutLen);

            return max;
        }

        /// <summary>
        /// 长度价值表
        /// </summary>
        public class LengthValueTable
        {
            /// <summary>
            /// 长度(米)
            /// </summary>
            public int Length { get; set; }

            /// <summary>
            /// 价值(元)
            /// </summary>

            public int Value { get; set; }

            public LengthValueTable(int length, int value)
            {
                Length = length;
                Value = value;
            }
        }

        /// <summary>
        /// 备忘录
        /// </summary>
        public class MemoDic
        {
            /// <summary>
            /// 长度价值备忘录,key为长度, value为价值
            /// </summary>
            public Dictionary<int, int> DicLengthValue { get; set; } = new Dictionary<int, int>();

            /// <summary>
            /// 长度为key钢管 最优时 最优时肯定要切的小段长度(value)
            /// </summary>
            public Dictionary<int, int> DicLengthChoose { get; set; } = new Dictionary<int, int>();
        }
    }
}