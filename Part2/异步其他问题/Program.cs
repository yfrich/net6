using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace 异步其他问题
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await foreach (var item in Test3())
            {
                Console.WriteLine(item);
            }
        }
        static IEnumerable<string> Test1()
        {
            IList<string> list = new List<string>();
            list.Add("hello");
            list.Add("bzy");
            list.Add("bzy.com");
            return list;
        }
        static IEnumerable<string> Test2()
        {
            yield return "hello";
            yield return "bzy";
            yield return "bzy.com";
        }
        static async IAsyncEnumerable<string> Test3()
        {
            yield return "hello";
            yield return "bzy";
            yield return "bzy.com";
        }
    }
    //接口定义问题。
    interface ITest
    {
        Task<int> GetCharCount(string file);
    }
    class Test : ITest
    {
        public async Task<int> GetCharCount(string file)
        {
            string s = await File.ReadAllTextAsync(file);
            return s.Length;
        }
    }
}
