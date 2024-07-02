using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwaitAsync2
{
    /// <summary>
    /// async、await 原理揭秘
    /// </summary>
    class Program
    {
        static string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\3.txt";

        static async Task Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string s = await httpClient.GetStringAsync("https://www.baidu.com");
                Console.WriteLine(s.Substring(0, 100));
            }
            string txt = "hello bzy";
            await File.WriteAllTextAsync(fileName, txt);

            Console.WriteLine("写入成功");

            string reads = await File.ReadAllTextAsync(fileName);
            Console.WriteLine("文件内容" + reads);
        }
    }
}
