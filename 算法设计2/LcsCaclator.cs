using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法设计
{
    public class LcsCaclator
    {
        public enum EnuArrow
        {
            /// <summary>向左</summary>
            Left,

            /// <summary>向上</summary>
            Top,

            /// <summary>左上</summary>
            LeftTop
        }

        public static void Run()
        {
            Console.WriteLine("最长子序列");
            var lcsCaclator = new LcsCaclator();
            lcsCaclator.Lcs("adcdadds", "2c1dseese");
        }

        public void Lcs(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)) return;
            int len1 = str1.Length + 1, len2 = str2.Length + 1;
            var c = new int[len1, len2]; //记录最长公共子序列
            var b = new EnuArrow[len1, len2]; //记录计算过程方向
            var s1 = str1.AsSpan();
            var s2 = str2.AsSpan();
            for (var i = 1; i < len1; i++)
            {
                for (var j = 1; j < len2; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        c[i, j] = c[i - 1, j - 1] + 1;
                        b[i, j] = EnuArrow.LeftTop;
                    }
                    else
                    {
                        if (c[i - 1, j] > c[i, j - 1])
                        {
                            c[i, j] = c[i - 1, j];
                            b[i, j] = EnuArrow.Top;
                        }
                        else
                        {
                            c[i, j] = c[i, j - 1];
                            b[i, j] = EnuArrow.Left;
                        }
                    }
                }
            }
            Console.WriteLine("计算表:");
            PrintLcsTable(s1, s2, c);

            Console.WriteLine("最长子序列结果:");
            PrintLcsReuslt(s1, s2, b);
        }

        private void PrintLcsReuslt(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2, EnuArrow[,] b)
        {
            var len1 = s1.Length;
            var len2 = s2.Length;

            Stack<char> stack = new Stack<char>();
            GetResult(stack, b, len1, len2, s1);

            var s = string.Join("", stack.ToList().ToArray());
            Console.WriteLine(s);
        }

        private void GetResult(Stack<char> stack, EnuArrow[,] b, int i, int j,
                                ReadOnlySpan<char> s1)
        {
            if (i == 0 || j == 0) return;
            switch (b[i, j])
            {
                case EnuArrow.Left:
                    GetResult(stack, b, i, j - 1, s1);
                    break;

                case EnuArrow.Top:
                    GetResult(stack, b, i - 1, j, s1);
                    break;

                case EnuArrow.LeftTop:
                    stack.Push(s1[i - 1]);
                    GetResult(stack, b, i - 1, j - 1, s1);
                    break;
            }
        }

        private void PrintLcsTable(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2, int[,] c)
        {
            var len1 = s1.Length + 1;
            var len2 = s2.Length + 1;
            Console.Write("  ");
            for (var i = 0; i < len2 - 1; i++)
            {
                Console.Write(s2[i]);
                Console.Write(" ");
            }
            Console.WriteLine();

            for (var i = 1; i < len1; i++)
            {
                for (var j = 0; j < len2; j++)
                {
                    if (j == 0 && j < len2 - 1 && i > 0)
                    {
                        Console.Write(s1[i - 1]);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(c[i, j]);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}