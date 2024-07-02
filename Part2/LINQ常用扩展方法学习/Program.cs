using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ常用扩展方法学习
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> list = new List<Employee>
            {
                new Employee { Id = 1, Name = "jerry", Age = 28, Gender = true, Salary = 5000 },
                new Employee { Id = 2, Name = "jim", Age = 33, Gender = false, Salary = 3000 },
                new Employee { Id = 3, Name = "lily", Age = 35, Gender = true, Salary = 9000 },
                new Employee { Id = 4, Name = "lucy", Age = 16, Gender = false, Salary = 2000 },
                new Employee { Id = 5, Name = "kimi", Age = 25, Gender = true, Salary = 1000 },
                new Employee { Id = 6, Name = "nancy", Age = 35, Gender = false, Salary = 8000 },
                new Employee { Id = 7, Name = "zack", Age = 35, Gender = true, Salary = 8500 },
                new Employee { Id = 8, Name = "jack", Age = 33, Gender = false, Salary = 8000 }
            };

            #region 扩展方法1 、2学习测试内容

            /*

//扩展方法1 Where 过滤数据
Console.WriteLine("方法：Where");
IEnumerable<Employee> item1 = list.Where(t => t.Age > 20);
foreach (Employee item in item1)
{
    Console.WriteLine(item);
}
//扩展方法2 Count() 计算和
Console.WriteLine("方法：Count");
Console.WriteLine(list.Count());
Console.WriteLine(list.Count(t => t.Age > 20 && t.Salary > 8000));

//扩展方法3 Any() 判断是否至少有一条数据满足条件，不传递参数 默认判断数组是否有数据
//Count() 也可以实现，但是Any的优势在于， Count会返回编译所有数据，然后根据结果去处理。
//Any的话就不需要，只要有一条符合，直接就返回了，参考yield语法(可能内部还不是yield实现的)
Console.WriteLine("方法：Any");
Console.WriteLine(list.Any(t => t.Salary > 10000));
Console.WriteLine(list.Any(t => t.Salary < 8000));

//扩展方法4 Single: 有且只有一条满足要求的数据。
//多条数据满足条件会报错，没有数据也会报错
Console.WriteLine("方法：Single");
//Console.WriteLine(list.Single());
Console.WriteLine(list.Single(t => t.Name == "jerry"));
//扩展方法5 SingleOrDefalut: 最多只有一条满足要求的数据
//多条会报错 没有数据不会报错 会返回默认值 
Console.WriteLine("方法：SingleOrDefault");
//Console.WriteLine(list.SingleOrDefault(t => t.Salary == 8000));
Console.WriteLine(list.SingleOrDefault(t => t.Name == "bzy") == null);
Console.WriteLine(list.SingleOrDefault(t => t.Name == "jerry"));
//扩展方法6 First:至少有一条，返回第一条 无数据会报错。
Console.WriteLine("方法：First");
//Console.WriteLine(list.First(t => t.Age > 300));
Console.WriteLine(list.First(t => t.Age > 30));
//扩展方法7 FirstOrDefault:返回第一条或者默认值
Console.WriteLine("方法：FirstOrDefault");
Console.WriteLine(list.FirstOrDefault(t => t.Age > 300) == null);
Console.WriteLine(list.FirstOrDefault(t => t.Age > 30));
//扩展方法8 OrderBy() 对数据进行正序排序
Console.WriteLine("方法：OrderBy");

var item8 = list.OrderBy(t => t.Age);
foreach (var item in item8)
{
    Console.WriteLine(item);
}
//扩展方法9 OrderByDescending() 对数据进行倒序排序
Console.WriteLine("方法：OrderByDescending");
var item9 = list.OrderByDescending(t => t.Age);
foreach (var item in item9)
{
    Console.WriteLine(item);
}
//可以对简单数据直接排序
Console.WriteLine("//可以对简单数据直接排序");
int[] nums = new int[] { 3, 9, 6, 5, 10, 7 };
var item92 = nums.OrderBy(t => t);
foreach (var item in item92)
{
    Console.WriteLine(item);
}
//也可以根据随机数排序 GUID 或者Radom
Console.WriteLine("也可以根据随机数排序 GUID 或者Radom");
var item93 = nums.OrderBy(t => Guid.NewGuid());
foreach (var item in item93)
{
    Console.WriteLine(item);
}
Console.WriteLine("按照名字的最后一个字母进行排序");
//按照名字的最后一个字母进行排序
var item94 = list.OrderBy(t => t.Name[t.Name.Length - 1]);
foreach (var item in item94)
{
    Console.WriteLine(item);
}
//还可以多规则排序
//ThenBy ThenByDescending
Console.WriteLine("还可以多规则排序");
var item95 = list.OrderBy(t => t.Age).ThenBy(t => t.Salary);
foreach (var item in item95)
{
    Console.WriteLine(item);
}

//扩展方法10 Skip 跳过N条数据
//扩展方法11 Take 获取N条数据 数据不够N 就取出已有所有数据

Console.WriteLine("Skip、Take");
var item10 = list.Skip(3).Take(2);
foreach (var item in item10)
{
    Console.WriteLine(item);
}

var items101 = list.Where(t => t.Age > 30).OrderBy(t => t.Age).Skip(1).Take(200);
foreach (var item in items101)
{
    Console.WriteLine(item);
}
//选择合适的方法，"防御性编程"

*/

            #endregion

            #region 聚合函数

            /*
            //Max获取最大值
            Console.WriteLine("获取年龄最大值");
            var maxAge = list.Max(t => t.Age);
            Console.WriteLine(maxAge);
            Console.WriteLine("获取ID>6 工资的最大值");
            var a = list.Where(t => t.Id > 6).Max(t => t.Salary);
            Console.WriteLine(a);
            Console.WriteLine("名字MAX");
            string s = list.Max(t => t.Name);//字符串大小比较算法是ASCLL 第一个字符比较，然后一次类推
            Console.WriteLine(s);

            //Average 求平均值函数
            double d1 = list.Where(t => t.Age > 30).Average(t => t.Salary);
            Console.WriteLine(d1);

            //Min() 获取最小值
            //Sum() 求和

            */
            //GroupBy 分组

            /*
            var grItems = list.GroupBy(t => t.Age);
            foreach (var item in grItems)
            {
                Console.WriteLine($"年龄组{item.Key}");
                Console.WriteLine($"人数：{item.Count()}");
                Console.WriteLine($"最高工资：{item.Max(t => t.Salary)}");
                Console.WriteLine($"平均工资：{item.Average(t => t.Salary)}");
            }

            //简化
            Console.WriteLine("简化：");
            var grItems2 = list.GroupBy(t => t.Age).Select(t => new { 年龄 = t.Key, 人数 = t.Count(), 最高工资 = t.Max(t => t.Salary), 平均工资 = t.Average(t => t.Salary) });
            foreach (var item in grItems2)
            {
                Console.WriteLine($"年龄组{item.年龄}");
                Console.WriteLine($"人数：{item.人数}");
                Console.WriteLine($"最高工资：{item.最高工资}");
                Console.WriteLine($"平均工资：{item.平均工资}");
            }
            */


            #endregion

            #region 扩展方法4

            /*
            //投影 映射
            Console.WriteLine("投影：");
            var select1 = list.Select(t => t.Age);
            foreach (var item in select1)
            {
                Console.WriteLine(item);
            }
            //性别转换 及输出
            Console.WriteLine("性别转换");
            var select2 = list.OrderBy(t => t.Gender).Select(t => t.Name + ":" + (t.Gender ? "男" : "女"));
            foreach (var item in select2)
            {
                Console.WriteLine(item);
            }
            //投影成对象(未编写) 匿名对象
            //var 高光时刻
            var obj1 = new { Name = "ddd", Salary = 3, AAA = "asdasd" };
            Console.WriteLine(obj1.AAA);
            Console.WriteLine(obj1.Name);
            */


            /*

            Console.WriteLine("投影与匿名类型：");
            var grItems2 = list.GroupBy(t => t.Age).Select(t => new { 年龄 = t.Key, 人数 = t.Count(), 最高工资 = t.Max(t => t.Salary), 平均工资 = t.Average(t => t.Salary) });
            foreach (var item in grItems2)
            {
                Console.WriteLine($"年龄组{item.年龄}");
                Console.WriteLine($"人数：{item.人数}");
                Console.WriteLine($"最高工资：{item.最高工资}");
                Console.WriteLine($"平均工资：{item.平均工资}");
            }
            */

            //集合转换

            /*

            IEnumerable<Employee> items1 = list.Where(t => t.Salary > 6000).ToList();
            List<Employee> itemList = items1.ToList();
            Employee[] array2 = items1.ToArray();
            foreach (var item in itemList)
            {
                Console.WriteLine(item);
            }
            */

            //链式调用
            /*
            var items = list.Where(t => t.Id > 2).GroupBy(t => t.Age).OrderBy(t => t.Key).Take(3)
                .Select(t => new { NL = t.Key, RS = t.Count(), GZ = t.Max(t => t.Salary), PJ = t.Average(t => t.Salary) });
            foreach (var item in items)
            {
                Console.WriteLine($"NL:{item.NL};RS:{item.RS};GZ:{item.GZ};PJ:{item.PJ};");
            }
            */

            #endregion

            #region 另一种倩影 查询语法


            var items = from u in list
                        where u.Salary > 3000
                        orderby u.Age
                        select new { MZ = u.Name, XB = u.Gender ? "男" : "女" };
            foreach (var item in items)
            {
                Console.WriteLine($"MZ={item.MZ};XB={item.XB}");
            }

            #endregion

        }
    }
}
