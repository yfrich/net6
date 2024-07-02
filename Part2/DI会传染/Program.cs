using System;
using Microsoft.Extensions.DependencyInjection;

namespace DI会传染
{
    class Program
    {
        static void Main(string[] args)
        {
            //写框架的人需要做的事儿
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<Controller>();
            services.AddScoped<ILog, LogImp1>();
            services.AddScoped<IStorage, StorageImp1>();
            //DI的好处 降低模块之前的耦合。
            //services.AddScoped<IConfig, ConfigImp1>();
            services.AddScoped<IConfig, DBConfigImp1>();

            using (var sp = services.BuildServiceProvider())
            {
                //容器创建的对象
                var c = sp.GetRequiredService<Controller>();
                c.Test();
            }
            Console.ReadKey();
        }
    }

    class Controller
    {
        private readonly ILog log;
        private readonly IStorage storage;

        public Controller(ILog log, IStorage storage)
        {
            this.log = log;
            this.storage = storage;
        }
        public void Test()
        {
            this.log.Log("开始上传");
            this.storage.Save("asdasd", "1.txt");
            this.log.Log("上传完毕");
        }
    }

    /// <summary>
    /// 日志服务
    /// </summary>
    interface ILog
    {
        public void Log(string msg);
    }
    class LogImp1 : ILog
    {
        public void Log(string msg)
        {
            Console.WriteLine($"日志{msg}");
        }
    }

    /// <summary>
    /// 配置服务
    /// </summary>
    interface IConfig
    {
        public string GetValue(string name);
    }
    class ConfigImp1 : IConfig
    {
        public string GetValue(string name)
        {
            return "";
        }
    }
    class DBConfigImp1 : IConfig
    {
        public string GetValue(string name)
        {
            Console.WriteLine("从数据库读取的配置");
            return "hello db";
        }
    }

    /// <summary>
    /// 云存储
    /// </summary>
    interface IStorage
    {
        public void Save(string content, string name);
    }
    class StorageImp1 : IStorage
    {
        private readonly IConfig config;
        /// <summary>
        /// 通过构造函数ID注入
        /// </summary>
        /// <param name="config"></param>
        public StorageImp1(IConfig config)
        {
            this.config = config;
        }
        public void Save(string content, string name)
        {
            string server = config.GetValue("server");
            Console.WriteLine($"向服务器{server}的文件名为{name}上传{content}");
        }
    }
}
