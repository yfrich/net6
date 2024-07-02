using MarkdownSharp;
using System.Globalization;
using System.Text;

namespace 中间件1
{
    public class MarkdownMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment webHost;

        public MarkdownMiddleware(RequestDelegate next, IWebHostEnvironment webHost)
        {
            this.next = next;
            this.webHost = webHost;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //根据webHost 获取静态文件资源
            string path = context.Request.Path.ToString();
            //只处理.MD的文件
            if (!path.EndsWith(".md", true, null))
            {
                await next.Invoke(context);
                return;
            }
            //WebRootFileProvider 是读取wwwroot  这个文件夹下的文件的
            var file = webHost.WebRootFileProvider.GetFileInfo(path);
            if (!file.Exists)
            {
                await next.Invoke(context);
                return;
            }
            //读取文件流
            using var stream = file.CreateReadStream();
            //获取流文件的编码
            Ude.CharsetDetector cdet = new Ude.CharsetDetector();
            cdet.Feed(stream);
            cdet.DataEnd();
            string charset = cdet.Charset ?? "UTF-8";
            //流复原
            stream.Position = 0;
            using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
            string mdText = await reader.ReadToEndAsync();
            Markdown mdk = new Markdown();
            string html = mdk.Transform(mdText);
            context.Response.ContentType = $"text/html;charset=UTF-8";
            await context.Response.WriteAsync(html);
        }
    }
}
