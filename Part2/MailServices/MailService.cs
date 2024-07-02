using ConfigServices;
using LogServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailServices
{
    public class MailService : IMailService
    {
        private readonly ILogProvider log;
        //private readonly IConfigService config;
        private readonly IConfigReader config;
        public MailService(ILogProvider log, IConfigReader config)
        {
            this.log = log;
            this.config = config;
        }

        public void Send(string title, string to, string body)
        {
            this.log.LogInfo("准备发送邮件");
            string smtpServer = this.config.GetValue("SmtpServer");
            string userName = this.config.GetValue("UserName");
            string password = this.config.GetValue("Password");
            Console.WriteLine($"邮件服务器地址{smtpServer},{userName},{password}");
            Console.WriteLine($"真发邮件啦 标题{title},{to},{body}");
            this.log.LogInfo("邮件发送完成");

        }
    }
}
