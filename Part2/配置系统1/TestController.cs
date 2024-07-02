using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置系统1
{
    class TestController
    {
        public readonly IOptionsSnapshot<Config> optConfig;
        public TestController(IOptionsSnapshot<Config> optConfig)
        {
            this.optConfig = optConfig;
        }
        public void Test()
        {
            Console.WriteLine(optConfig.Value.Age);
            Console.WriteLine("*************");
            Console.WriteLine(optConfig.Value.Name);
            //Console.WriteLine(optConfig.Value.Proxy.Address);
            //Console.WriteLine(optConfig.Value.Proxy.Port);
            //Console.WriteLine(string.Join(",", optConfig.Value.Proxy.Ids));
        }
    }
}
