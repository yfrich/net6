using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置系统1
{
    class Test2
    {
        public readonly IOptionsSnapshot<Proxy> optProxy;
        public Test2(IOptionsSnapshot<Proxy> optProxy)
        {
            this.optProxy = optProxy;
        }
        public void Test()
        {
            Console.WriteLine(optProxy.Value.Address);
            Console.WriteLine("*********************");
            Console.WriteLine(optProxy.Value.Address);
        }
    }
}
