using Intf1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldCode
{
    public class MyDataPrivider1 : IMyDataProvider
    {
        public IEnumerable<EmailInfo> GetEmailsToSent()
        {
            string[] lines = File.ReadAllLines("f:/1.txt");
            foreach (string line in lines)
            {
                string[] segments = line.Split('|');
                string email = segments[0];
                string title = segments[1];
                string body = segments[2];
                yield return new EmailInfo(email, title, body);
            }
        }
    }
}
