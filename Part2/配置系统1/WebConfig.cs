using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置系统1
{
    class WebConfig
    {
        public ConnectStr Conn1 { get; set; }
        public ConnectStr ConnTest { get; set; }
        public Config Config { get; set; }
    }
    class ConnectStr
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}
