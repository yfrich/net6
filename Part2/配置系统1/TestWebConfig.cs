using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置系统1
{
    class TestWebConfig
    {
        private readonly IOptionsSnapshot<WebConfig> optWC;
        public TestWebConfig(IOptionsSnapshot<WebConfig> optWC)
        {
            this.optWC = optWC;
        }
        public void Test()
        {
            var wc = optWC.Value;

            Console.WriteLine(wc.Conn1.ConnectionString);
            Console.WriteLine(wc.ConnTest.ConnectionString);
            Console.WriteLine(wc.Config.Age);
            Console.WriteLine(wc.Config.Proxy.Address);
        }
    }
}
