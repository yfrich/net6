using System;
using MailServices;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAappMailSender
{
    class Program
    { 
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            //services.AddScoped<IConfigService, EnVarConfigService>();
            //不是单例模式，且不是简单的对象，需要初始化赋值，就需要通过回调的方式创建
            //services.AddScoped(typeof(IConfigService), s => new IniFileConfigService { FilePath = "mail.ini" });
            //配置读取顺序就是注册顺序 依次最后一个有值就取最后的。
            //注入Ini配置获取
            services.AddIniFileConfig("mail.ini");
            //注入EnVar配置获取
            services.AddEnVarConfig();
            //注入配置文件的中心服务
            services.AddLayerConfig();
            //services.AddScoped<ILogProvider, ConsoleLogProvider>();
            services.AddScoped<IMailService, MailService>();
            //services.AddConsoleLog();//提供一个Add方法，能够自动提示出来，不用using
            //为了能够直接.出来，不让用户记住实现类的接口名字，实现类的名字是什么。
            services.AddConsoleLog();
            using (var sp = services.BuildServiceProvider())
            {
                //第一个根对象只能用ServiceLocator
                var mailService = sp.GetRequiredService<IMailService>();
                mailService.Send("hello", "trum@usa.gov", "懂王你好");
            }
            Console.Read();
        }
    }
}
