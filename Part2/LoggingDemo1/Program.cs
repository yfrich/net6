using Exceptionless;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using System;
using SystemServices;

namespace LoggingDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            ExceptionlessClient.Default.Startup("iLpAVaIhxWxvWz78QhL7Ptv7N3j5PFS4lJUeqYN1");
            ServiceCollection services = new ServiceCollection();
            //注册Logging 到DI容易。 也就是注入
            services.AddLogging(logBuilder =>
            {
                //将日志输出到控制台。
                //这里具体定义日志怎么输出。
                //具体输出什么，由业务类定义。
                //默认配置只输出大于>Error的
                //logBuilder.AddConsole();
                //Windows服务日志 不行，报错 不知道为啥 net core跨平台 几乎不用的
                //logBuilder.AddEventLog();
                //输出到Nlog
                //logBuilder.AddNLog();
                //设置日志输出的级别 可以通过配置文件来设置
                //logBuilder.SetMinimumLevel(LogLevel.Debug);
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console(new JsonFormatter())
                .WriteTo.Exceptionless()
                .CreateLogger();
                logBuilder.AddSerilog();
            });

            services.AddScoped<Test1>();
            services.AddScoped<Test2>();

            using (var sp = services.BuildServiceProvider())
            {
                //for (int i = 0; i < 10000; i++)
                //{
                var test1 = sp.GetRequiredService<Test1>();
                test1.Test();

                var test2 = sp.GetRequiredService<Test2>();
                test2.Test();
                //}
            }
        }
        static void TT(ILoggingBuilder builder)
        {

        }
    }
}
