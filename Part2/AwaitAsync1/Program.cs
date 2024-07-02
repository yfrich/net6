using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsync1
{
    class Program
    {
        /*
        //异步方法调用
        static async Task Main(string[] args)
        {
            //同步
            //string fileName = "f:\temp\a";
            //string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt";
            //File.WriteAllText(fileName, "hello");
            //string s= File.ReadAllText(fileName);
            //Console.WriteLine(s);

            //异步
            string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt";
            await File.WriteAllTextAsync(fileName, "hello");
            string s = await File.ReadAllTextAsync(fileName);
            Console.WriteLine(s);

            //如果不包含await
            //string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt";
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 10000; i++)
            //{
            //    sb.AppendLine("hellp");
            //}
            //File.WriteAllTextAsync(fileName, sb.ToString());
            //string s = await File.ReadAllTextAsync(fileName);
            //Console.WriteLine(s);
        }
        */
        
        /*
        //异步方法编写
        static async Task Main(string[] args)
        {
            string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt";

            int i = await DownloadHtmlAsync("https://www.youzack.com", fileName);
            Console.WriteLine("OK" + i);
        }
        */
        

        /*
        //不带有返回值的
        static async Task DownloadHtmlAsync(string url, string filename)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string htmlText = await httpClient.GetStringAsync(url);
                await File.WriteAllTextAsync(filename, htmlText);
            }
        }
        static async Task<int> DownloadHtmlAsync(string url, string filename)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string htmlText = await httpClient.GetStringAsync(url);
                await File.WriteAllTextAsync(filename, htmlText);
                //返回int值 会自动转换到Task<int>
                return htmlText.Length;
            }
        }
        */

        /*
        //只支支持同步，没有异步
        static void Main(string[] args)
        {
            string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt";
            //有返回值的同步下调用异步。 .Result
            //string s = File.ReadAllTextAsync(fileName).Result;
            //Console.WriteLine(s.Substring(0, 20));
            //无返回值的同步下调用异步 .Wait()
            File.WriteAllTextAsync(fileName, "asasdasdasdasd").Wait();
        }
        */


        //异步委托 lambda 前加上async
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(async (obj) =>
            {
                //int i = 0;
                //while (i < 10000)
                //{
                //    Console.WriteLine("xxxxxx");
                //    i++;
                //}
                string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\1.txt";
                await File.WriteAllTextAsync(fileName, "hello");
            }));
            Console.Read();
        }

    }
}
