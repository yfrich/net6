using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace IOC1
{
    class Program
    {
        static void Main1(string[] args)
        {
            /*
            ITestService t = new TestServiceImpl();
            t.Name = "sasd";
            t.SayHi();
            */

            //创建对象并注册服务
            ServiceCollection services = new ServiceCollection();
            //生命周期学习
            //瞬态
            //services.AddTransient<TestServiceImpl>();
            //范围
            //services.AddScoped<TestServiceImpl>();
            //单例
            services.AddSingleton<TestServiceImpl>();
            //ServiceProvider== 服务定位器
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                /*
                //验证生命周期
                //瞬态
                TestServiceImpl t = sp.GetService<TestServiceImpl>();
                t.Name = "lily";
                t.SayHi();
                TestServiceImpl t1 = sp.GetService<TestServiceImpl>();
                Console.WriteLine(object.ReferenceEquals(t, t1));
                t1.Name = "tom";
                t1.SayHi();
                t.SayHi();

                */


                //范围 单例
                TestServiceImpl tt1;
                using (IServiceScope scope1 = sp.CreateScope())
                {
                    //在scope中获取Scope相关的对象，而不是 ServiceProvider sp
                    TestServiceImpl t = scope1.ServiceProvider.GetService<TestServiceImpl>();
                    t.Name = "lily";
                    t.SayHi();
                    TestServiceImpl t1 = scope1.ServiceProvider.GetService<TestServiceImpl>();
                    Console.WriteLine(object.ReferenceEquals(t, t1));
                    tt1 = t1;
                }

                using (IServiceScope scope2 = sp.CreateScope())
                {
                    //在scope中获取Scope相关的对象，而不是 ServiceProvider sp
                    TestServiceImpl t = scope2.ServiceProvider.GetService<TestServiceImpl>();
                    t.Name = "lily";
                    t.SayHi();
                    TestServiceImpl t1 = scope2.ServiceProvider.GetService<TestServiceImpl>();
                    Console.WriteLine(object.ReferenceEquals(t, t1));
                    Console.WriteLine(object.ReferenceEquals(tt1, t1));
                }


            }
        }

        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            //ITestService 就是服务类型  TestServiceImpl就是实现类型
            services.AddScoped<ITestService, TestServiceImpl>();
            services.AddScoped<ITestService, TestServiceImp2>();
            //services.AddScoped(typeof(ITestService), typeof(TestServiceImpl));
            //services.AddSingleton<ITestService, TestServiceImpl>();
            //单例模式可以直接指定对象，复杂的初始化对象可以在此处处理。
            //services.AddSingleton(typeof(ITestService), new TestServiceImpl());
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                /*
                ITestService ts1 = sp.GetService<ITestService>();
                ts1.Name = "tom";
                ts1.SayHi();
                Console.WriteLine(ts1.GetType());
                */

                //此方式不行，注册什么服务类型就拿什么类型， 不能使用实现类型
                //GetService(泛型)如果找不到服务，就返回null
                /*
                TestServiceImpl ts1 = sp.GetService<TestServiceImpl>();
                */
                //GetService非泛型 一般不用，编写框架时使用
                /*
                ITestService ts1 = (ITestService)sp.GetService(typeof(ITestService));
                */
                //GetRequiredService
                /*
                ITestService ts1 = sp.GetRequiredService<ITestService>();
                */
                //Required：必须的，如果找不到服务会抛出异常
                //显示类型转换和as
                /*
                TestServiceImpl ts1 = sp.GetRequiredService<TestServiceImpl>();
               
                ts1.Name = "tom";
                ts1.SayHi();
                Console.WriteLine(ts1.GetType());

                */

                //如果服务没有 返回数组长度为0，所以不需要支持Required
                /*
                IEnumerable<ITestService> tests = sp.GetServices<ITestService>();
                foreach (var item in tests)
                {
                    Console.WriteLine(item.GetType());
                }
                */
                //如果注册多个服务，在使用Service 则会取出最后一个注册的服务
                var t = sp.GetService<ITestService>();
                Console.WriteLine(t.GetType());
            }
        }
    }
    //服务类型
    public interface ITestService
    {
        public string Name { get; set; }
        public void SayHi();
    }
    //实现类型
    public class TestServiceImpl : ITestService, IDisposable
    {
        public string Name { get; set; }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }

        public void SayHi()
        {
            Console.WriteLine($"Hi, I'm {Name}");
        }
    }
    public class TestServiceImp2 : ITestService
    {
        public string Name { get; set; }
        public void SayHi()
        {
            Console.WriteLine($"你好，我是{Name}");
        }
    }
}
