using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置系统1
{
    static class FxConfigExtensions
    {
        public static IConfigurationBuilder AddFxConfg(this IConfigurationBuilder cb, string path = null)
        {
            if (path == null)
            {
                path = "web.config";
            }
            cb.Add(new FxConfigSource() { Path = path });
            return cb;
        }
    }
}
