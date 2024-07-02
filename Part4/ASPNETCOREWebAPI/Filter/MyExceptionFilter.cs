using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNETCOREWebAPI.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment webHost;

        public MyExceptionFilter(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            //当前方法的名称以及参数信息 context.ActionDescriptor
            //代表异常信息对象context.Exception
            //如果给context.ExceptionHandled=true 则其他ExceptionFilter不会在执行(参考日志logexceptionfilter)
            //context.Result的值会被输出给客户端
            //根据开发环境决定输出的日志信息
            string msg;
            if (webHost.IsDevelopment())
            {
                msg = context.Exception.ToString();
            }
            else
            {
                msg = "服务器端发生未处理异常";
            }
            ObjectResult objectResult = new ObjectResult(new { code = 500, message = msg });
            context.Result = objectResult;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
