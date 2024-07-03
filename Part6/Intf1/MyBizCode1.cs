using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intf1
{
    public class MyBizCode1
    {
        private IEmailSender _emailSender;
        private IMyDataProvider _myDataProvider;

        public MyBizCode1(IEmailSender emailSender, IMyDataProvider myDataProvider)
        {
            _emailSender = emailSender;
            _myDataProvider = myDataProvider;
        }
        public async Task DoItAsync()
        {
            var items = _myDataProvider.GetEmailsToSent();
            foreach (var item in items)
            {
                await _emailSender.SendAsync(item.Email, item.Titile, item.Body);
                await Task.Delay(1000);
            }
        }
    }
}
