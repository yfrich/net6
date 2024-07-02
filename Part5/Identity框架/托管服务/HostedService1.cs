
using Microsoft.Extensions.DependencyInjection;

namespace Identity框架.托管服务
{
    public class HostedService1 : BackgroundService
    {
        /*
        private readonly TestService testService;

        public HostedService1(TestService testService)
        {
            this.testService = testService;
        }
        */
        private IServiceScope serviceScope;
        public HostedService1(IServiceScopeFactory serviceScopeFactory)
        {
            serviceScope = serviceScopeFactory.CreateScope();
        }
        /// <summary>
        /// 一定记得要释放资源
        /// </summary>
        public override void Dispose()
        {
            serviceScope.Dispose();
            base.Dispose();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //手动获取服务
                var testService = serviceScope.ServiceProvider.GetRequiredService<TestService>();
                Console.WriteLine($"HostedService1 启动:{testService.Add(1, 3)}");
                await Task.Delay(3000);//不要用Sleep

                string file = await File.ReadAllTextAsync("f:/1.txt");
                //要做充足的预防，不要直接用try cache兜底
                //File.Exists("F:/1.txt");
                Console.WriteLine("文件读取完成");
                await Task.Delay(15000);//不要用Sleep
                Console.WriteLine(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"服务中出现未处理异常:{ex}");
            }
        }
    }
}
