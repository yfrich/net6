using Intf1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldCode
{
    public class MyEmailSender : IEmailSender
    {
        public Task SendAsync(string email, string title, string body)
        {
            Console.WriteLine($"发送邮件：{email},{title},{body}");//smtp/调用服务接口
            return Task.CompletedTask;
        }
    }
}
