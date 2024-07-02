using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ原理揭秘
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 12, 23, 12, 4, 5, 64, 1, 1, 332, 65, 12 };
            //Where 方法会便利集合中每个元素，对每个元素
            //都调用 a=>a>10 这个表达式判断一下是否为true
            //如果为true，则把这个放到返回的集合中
            //IEnumerable<int> filterNums = nums.Where(t => t > 10);
            //IEnumerable<int> filterNums2 = MyWhere2(nums, t => t > 10);
            //dyanamic 与JS 差不多 弱类型。 var 类型是推断类型，会根据你复制的数据 编译器自动编译成对应类型。

            var result = MyWhere2(nums, t => t > 10);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
        static IEnumerable<int> MyWhere1(IEnumerable<int> items, Func<int, bool> f)
        {
            List<int> result = new List<int>();
            foreach (var item in items)
            {
                if (f(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
        static IEnumerable<int> MyWhere2(IEnumerable<int> items, Func<int, bool> f)
        {
            foreach (var item in items)
            {
                if (f(item))
                {
                    Console.WriteLine($"MyWhere:{item}");
                    yield return item;
                }
            }
        }
    }
}
