using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Identity框架.SignalR案例导入英汉词典
{
    [AllowAnonymous]
    public class ImportDictHub : Hub
    {
        private IServiceScope serviceScope;
        public ImportDictHub(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScope = serviceScopeFactory.CreateScope();
        }

        public Task ImportEcDict()
        {
            var importExecutor = serviceScope.ServiceProvider.GetRequiredService<ImportExecutor>();
            _ = importExecutor.ExecuteAsync(this.Context.ConnectionId);

            return Task.CompletedTask;
        }
    }
}
