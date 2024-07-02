using System;
using System.Collections.Generic;
using System.Linq;

namespace linq解决面试题
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            int j = 5;
            int i = 8;
            int k = 6;
            */

            //linq方式
            /*
            int[] nums = new int[] { i, j, k };
            int max = nums.Max();
            Console.WriteLine(max);
            */

            /*
            int max = Math.Max(i, Math.Max(j, k));
            Console.WriteLine(max);
            */

            //计算一个字符串的平均值

            /*
             
            string str = "61,90,100,99,18,22,38,66,80,93,55,50,89";

            Console.WriteLine("未用LINQ");
            string[] strs = str.Split(',');
            int count = strs.Length;
            double sumNum = 0;
            for (int i = 0; i < strs.Length; i++)
            {
                sumNum += int.Parse(strs[i]);
            }
            Console.WriteLine(sumNum / count);

            Console.WriteLine("使用LINQ");
            var list = str.Split(',').Select(t => int.Parse(t)).Average();
            Console.WriteLine(list);

            */

            //统计一个字符串中每个字母出现的频率(忽略大小写)，
            //然后按照从高到低的顺序输出出现频率高于两次的单词和其出现的频率；
            string str = "asdWEQKASDsfqwrasdasQWEQWZXCZQEQWEASFfgeypnbYUPMOB";
            //var items = str.ToLower().ToCharArray().GroupBy(t => t).Where(t => t.Count() > 2).OrderByDescending(t => t.Count()).Select(t => new { DC = t.Key, PL = t.Count() });
            var items = str.Where(t => char.IsLetter(t)).Select(t => char.ToLower(t)).GroupBy(t => t).Where(t => t.Count() > 2).OrderByDescending(t => t.Count()).Select(t => new { DC = t.Key, PL = t.Count() });
            foreach (var item in items)
            {
                Console.WriteLine($"单词:{item.DC};频率:{item.PL}");
            }
        }
    }
}
