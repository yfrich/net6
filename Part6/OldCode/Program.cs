using Intf1;
using Microsoft.Extensions.DependencyInjection;
using OldCode;

/*
string[] lines = File.ReadAllLines("f:/1.txt");
foreach (string line in lines)
{
    string[] segments = line.Split('|');
    string email = segments[0];
    string title = segments[1];
    string body = segments[2];
    Console.WriteLine($"发送邮件：{email},{title},{body}");//smtp/调用服务接口
}
*/
ServiceCollection services = new ServiceCollection();
services.AddScoped<MyBizCode1>();
services.AddScoped<IEmailSender, MyEmailSender>();
//services.AddScoped<IMyDataProvider, MyDataPrivider1>();
services.AddScoped<IMyDataProvider, MyDataPrividerMock>();
var sp = services.BuildServiceProvider();
var code1 = sp.GetRequiredService<MyBizCode1>();
await code1.DoItAsync();