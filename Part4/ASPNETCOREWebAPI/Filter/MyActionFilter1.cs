using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNETCOREWebAPI.Filter
{
    public class MyActionFilter1 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //方法执行前的代码，前后由 await next()分隔
            Console.WriteLine("MyActionFilter1:前代码");
            ActionExecutedContext t = await next();
            //方法执行后执行的代码
            if (t.Exception != null)
            {
                Console.WriteLine("MyActionFilter1:后发生异常了");
            }
            else
            {
                Console.WriteLine("MyActionFilter1:后执行成功");
            }
        }
    }
}
