using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ASPNETCOREWebAPI.Filter
{
    /// <summary>
    /// 限流过滤器实现
    /// </summary>
    public class RateLimitActionFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache memoryCache;
        public RateLimitActionFilter(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //用户请求的IP地址
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string cacheKey = $"lastvisittick_{ip}";
            long? lastVisit = memoryCache.Get<long?>(cacheKey);
            //通过内存缓存校验是否超出频率
            if (lastVisit == null || Environment.TickCount64 - lastVisit > 1000)
            {
                //避免长期不访问的用户，占用缓存的内存，设置一个10秒的请求
                memoryCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));
                await next();
            }
            else
            {
                ObjectResult objectResult = new ObjectResult("访问太频繁") { StatusCode = 429 };
                context.Result = objectResult;
            }
        }
    }
}
