using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsync3
{
    /// <summary>
    /// 异步调用内部线程切换
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("之前：" + Thread.CurrentThread.ManagedThreadId);
            double r = await Calc1Async(5000);
            Console.WriteLine($"r={r}");
            Console.WriteLine("之后：" + Thread.CurrentThread.ManagedThreadId);
        }
        //自定义异步方法内部未调用异步方法，需要使用Task.Run() 来主动进行线程切换实现异步。
        public static async Task<double> CalcAsync(int n)
        {
            //Console.WriteLine("CalcAsync：" + Thread.CurrentThread.ManagedThreadId);
            //double result = 0;
            //Random random = new Random();
            //for (int i = 0; i < n * n; i++)
            //{
            //    result += random.NextDouble();
            //}
            //return result;

            return await Task.Run(() =>
            {
                Console.WriteLine("CalcAsync：" + Thread.CurrentThread.ManagedThreadId);
                double result = 0;
                Random random = new Random();
                for (int i = 0; i < n * n; i++)
                {
                    result += random.NextDouble();
                }
                return result;
            });
        }

        public static Task<double> Calc1Async(int n)
        {

            return Task.Run(() =>
           {
               Console.WriteLine("CalcAsync：" + Thread.CurrentThread.ManagedThreadId);
               double result = 0;
               Random random = new Random();
               for (int i = 0; i < n * n; i++)
               {
                   result += random.NextDouble();
               }
               //return result;
               return Task.FromResult(result);
           });
        }
    }
}
