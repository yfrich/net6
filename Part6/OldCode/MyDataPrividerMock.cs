using Intf1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldCode
{
    public class MyDataPrividerMock : IMyDataProvider
    {
        public IEnumerable<EmailInfo> GetEmailsToSent()
        {
            yield return new EmailInfo("1@test.com", "1", "2");
            yield return new EmailInfo("1@test.com", "1", "2");
            yield return new EmailInfo("1@test.com", "1", "2");
        }
    }
}
