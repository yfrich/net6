
using Microsoft.EntityFrameworkCore;

namespace Identity框架.托管服务
{
    public class ScheduledService : BackgroundService
    {
        private IServiceScope serviceScope;

        public ScheduledService(IServiceScopeFactory serviceScopeFactory)
        {
            //通过工厂方式注入。把工厂给你 你拿来用
            this.serviceScope = serviceScopeFactory.CreateScope();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var myDb = this.serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();
                //stoppingToken.IsCancellationRequested 判断服务是否关闭 为true就关闭了
                while (!stoppingToken.IsCancellationRequested)
                {
                    long c = await myDb.Users.LongCountAsync();
                    await File.WriteAllTextAsync("f:/hostService.txt", c.ToString());
                    Console.WriteLine($"导出成功:{DateTime.Now}");
                    await Task.Delay(5000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
