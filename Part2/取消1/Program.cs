using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace 取消1
{
    class Program
    {
        static async Task Main1(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            await Download3Async("https://www.youzack.com", 100, cts.Token);
        }
        //手动输入代码后进行终止
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Download4Async("https://www.youzack.com", 100, cts.Token);
            while (Console.ReadLine() != "q")
            {

            }
            cts.Cancel();
            Console.ReadLine();
        }

        static async Task Download1Async(string url, int n)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now}:{html.Substring(0, 100)}");
                }
            }
        }
        //手动处理cancellationToken 如何停止 什么时候停止
        //优点：可以自己控制 缺点，必须等待请求完成后才能进行校验，请求可能就是很耗时。但是也只能等待完成后
        static async Task Download2Async(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now}:{html.Substring(0, 100)}");
                    //响应取消 使用时 用这种方法
                    //if (cancellationToken.IsCancellationRequested)
                    //{
                    //    Console.WriteLine("请求被取消");
                    //    break;
                    //}
                    //演示学习用 
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
        //携带cancellationToken 的异步调用 把token给异步方法，由异步方法决定。
        //缺点：无法控制 优点：异步请求时间到了5秒会直接返回。
        static async Task Download3Async(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    var resp = await httpClient.GetAsync(url, cancellationToken);
                    string html = await resp.Content.ReadAsStringAsync();
                    Console.WriteLine($"{DateTime.Now}:{html.Substring(0, 100)}");
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("请求被取消");
                        break;
                    }
                }
            }
        }
        //主动停止
        static async Task Download4Async(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    //var resp = await httpClient.GetAsync(url, cancellationToken);
                    string html = await httpClient.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now}:{html.Substring(0, 100)}");
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("请求被取消");
                        break;
                    }
                }
            }
        }
    }
}
