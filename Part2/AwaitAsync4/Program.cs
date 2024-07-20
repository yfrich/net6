using System;
using System.IO;
using System.Threading.Tasks;

namespace AwaitAsync4
{
    /// <summary>
    /// 异步编程：为什么有的异步方法没有标记async
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            string s = await ReadAsync(1);
            Console.WriteLine(s);
        }
        //static async Task<string> ReadAsync(int num)
        //{
        //    switch (num)
        //    {
        //        case 0:
        //            return await File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt");
        //        case 1:
        //            return await File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt");
        //        default:
        //            throw new ArgumentException();
        //    }
        //}
        //方法内部不会起线程
        static Task<string> ReadAsync(int num)
        {
            switch (num)
            {
                case 0:
                    return File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt");
                case 1:
                    return File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt");
                default:
                    //throw new ArgumentException();
                    return Task.FromResult("这就是异步方法定义");
            }
        }
        //方法内部会另起一个线程
        static async Task<string> Read2Async(int num)
        {
            switch (num)
            {
                case 0:
                    string s = await File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt");
                    return s + "XXXX";
                case 1:
                    return await File.ReadAllTextAsync(@"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt");
                default:
                    //throw new ArgumentException();
                    return "ss";
            }
        }
    }
}
