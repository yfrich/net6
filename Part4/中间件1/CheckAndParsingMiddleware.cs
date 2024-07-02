using Dynamic.Json;
using System.Text.Json;

namespace 中间件1
{
    public class CheckAndParsingMiddleware
    {
        private readonly RequestDelegate next;

        public CheckAndParsingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string password = context.Request.Query["password"];
            if (password == "123")
            {
                if (context.Request.HasJsonContentType())
                {
                    var stream = context.Request.BodyReader.AsStream();
                    dynamic? obj = await DJson.ParseAsync(stream);
                    //system.text.json (.net6 ) 目前不支持转换成dynamic类型的
                    //视频其他人说支持，我没找到
                    //JsonSerializer.Deserialize(stream,typeof(dynamic));
                    context.Items["BodyJson"] = obj;
                }
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
