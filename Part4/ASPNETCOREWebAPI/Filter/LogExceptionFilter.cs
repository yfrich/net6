using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNETCOREWebAPI.Filter
{
    /// <summary>
    /// 日志异常过滤器
    /// </summary>
    public class LogExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            //context.ExceptionHandled = true; 不需要设置，其他异常类会处理，此异常类值进行日志记录
            return File.AppendAllTextAsync("f:/error.log", context.Exception.ToString());
        }
    }
}
