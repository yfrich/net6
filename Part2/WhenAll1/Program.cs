using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace WhenAll1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Task<string> t1 = File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt");
            //Task<string> t2 = File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt");
            //Task<string> t3 = File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\3.txt");

            ////等三个都执行完成后在统一返回
            //string[] strs = await Task.WhenAll(t1, t2, t3);
            //string s1 = strs[0];
            //string s2 = strs[1];
            //string s3 = strs[2];
            //Console.WriteLine($"文件1{s1}");
            //Console.WriteLine($"文件2{s2}");
            //Console.WriteLine($"文件3{s3}");

            string[] files = Directory.GetFiles(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile");
            Task<int>[] countTasks = new Task<int>[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i];
                Task<int> t = ReadCharCountAsync(fileName);
                countTasks[i] = t;
            }
            //所有任务完成后才全部返回
            int[] counts = await Task.WhenAll(countTasks);
            //完成任何一个Task就返回
            //int[] counts = await Task.WhenAny(countTasks);
            int c = counts.Sum();
            Console.WriteLine($"合计字符数量：{c}");
        }


        static async Task<int> ReadCharCountAsync(string fileName)
        {
            string s = await File.ReadAllTextAsync(fileName);
            return s.Length;
        }
    }
}
