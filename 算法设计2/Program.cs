// See https://aka.ms/new-console-template for more information
using 算法设计2;

//Console.WriteLine("最长子序列");
//var lcsCaclator = new LcsCaclator();
//lcsCaclator.Lcs("adcdadds", "2c1dseese");

var bag = new BagQuestion();
var n = 5; //n个物品
var weights = new int[] { 0, 1, 2, 5, 6, 7 };     //物品对应的重量
var values = new int[] { 0, 1, 6, 18, 22, 28 }; //物品对应的价值
var maxWeight = 11; //背包可容重量
var m = bag.GetBest(maxWeight, weights, values, n);
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

Console.WriteLine($"所以可装重量{ maxWeight }的背包, 最大可装价值: { m[n, maxWeight]}");