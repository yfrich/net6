using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置系统1
{
    /// <summary>
    /// 主要是提供参数使用
    /// </summary>
    class FxConfigSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new FxConfigProvider(this);
        }
    }
}
