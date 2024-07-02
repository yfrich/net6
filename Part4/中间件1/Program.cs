using Microsoft.AspNetCore.Identity;
using �м��1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
app.MapGet("/", () => "Hello World!");
app.MapGet("/Test", () => "richFu");
*/

app.Map("/test", async (t) =>
{
    t.UseMiddleware<CheckAndParsingMiddleware>();
    t.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("1 end <br/>");
    });
    t.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("2 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("2 end <br/>");
    });
    t.UseMiddleware<Test1Middleware>();
    t.Run(async context =>
    {
        await context.Response.WriteAsync("run  <br/>");
        dynamic? obj = context.Items["BodyJson"];
        if (obj != null)
        {
            await context.Response.WriteAsync($"{obj} <br/>");
        }
    });
});
//ע��markdown�м��
app.UseMiddleware<MarkdownMiddleware>();
app.UseStaticFiles();
app.Run();
