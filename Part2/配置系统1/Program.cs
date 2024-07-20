using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace 配置系统1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            //注册TestController 到DI容器中
            services.AddScoped<TestController>();
            //注册Test2 到DI容器中
            services.AddScoped<Test2>();
            //注册TestWebConfig到容器
            services.AddScoped<TestWebConfig>();



            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();



            //参数说明 path 文件名 optional 是否可以选（是否必须存在） reloadOnChange:配置文件修改后，是否立即
            //更新读取最新的配置。不需要重新启动。
            //用户机密 避免泄露
            configurationBuilder.AddUserSecrets<Program>();
            //Json方式
            configurationBuilder.AddJsonFile("config.json", optional: true, reloadOnChange: true);
            //命令行形式
            configurationBuilder.AddCommandLine(args);
            //环境变量方式 重载参数 prefix ：环境变量前缀
            configurationBuilder.AddEnvironmentVariables("C1_");
            //configurationBuilder.Add(new FxConfigSource() { Path = "web.config" });
            //自己建造的 web.config 配置文件读取
            configurationBuilder.AddFxConfg();



            //使用Zack的AnyDB获取配置信息

            /*
            string connStr = "Server=.;database=aspnetcore;uid=sa;pwd=sa";
            configurationBuilder.AddDbConfiguration(() => new SqlConnection(connStr), reloadOnChange: true, reloadInterval: TimeSpan.FromSeconds(2));
            */




            IConfigurationRoot configRoot = configurationBuilder.Build();

            //自定义的读取WebConfig的

            /*
            services.AddOptions().Configure<WebConfig>(t => configRoot.Bind(t));
            using (var sp = services.BuildServiceProvider())
            {
                var c = sp.GetRequiredService<TestWebConfig>();
                c.Test();
            }
            */



            //DI注册 Options 然后把config绑定到跟节点上！！！！！！！
            //可以绑定多个节点

            services.AddOptions()
                .Configure<Config>(t => configRoot.Bind(t));
            //.Configure<Proxy>(t => configRoot.GetSection("proxy").Bind(t));

            
            using (var sp = services.BuildServiceProvider())
            {
                while (true)
                {
                    //需要重新创建作用域
                    using (var scope = sp.CreateScope())
                    {
                        var c = scope.ServiceProvider.GetRequiredService<TestController>();
                        c.Test();
                        Console.WriteLine("改一下Age");
                        Console.ReadKey();
                        c.Test();

                        //var c2 = scope.ServiceProvider.GetRequiredService<Test2>();
                        //c2.Test();
                    }

                    Console.WriteLine("点击任意键继续");
                    Console.ReadKey();
                }
            }



            //简单读取配置
            /*            
            string name = configRoot["name"];
            string proxyAddress = configRoot.GetSection("proxy:Address").Value;
            Console.WriteLine($"获取的配置信息{name},获取的子节点对象信息{proxyAddress}");
            Console.ReadKey();
            */

            //绑定读取配置
            /*
            var proxy = configRoot.GetSection("proxy").Get<Proxy>();
            Console.WriteLine($"address:{proxy.Address},port:{proxy.Port}");
            Console.ReadKey();
            */


            /*
            var config = configRoot.Get<Config>();
            Console.WriteLine($"获取的配置信息{config.Name},获取的子节点对象信息{config.Age}");
            Console.WriteLine($"address:{config.Proxy.Address},port:{config.Proxy.Port}");
            Console.ReadKey();
            */

        }
    }
    class Config
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Proxy Proxy { get; set; }

    }
    class Proxy
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public int[] Ids { get; set; }
    }
}
