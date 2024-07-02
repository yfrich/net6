namespace 中间件1
{
    public class Test1Middleware
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 定义构造函数必须要有 RequestDelegate 参数
        /// </summary>
        /// <param name="next"></param>
        public Test1Middleware(RequestDelegate next)
        {
            this.next = next;
        }
        /// <summary>
        /// 必须要定义该方法，且参数为 HttpContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Test1Middleware start<br/>");
            await next.Invoke(context);
            await context.Response.WriteAsync("Test1Middleware end<br/>");
        }
    }
}
