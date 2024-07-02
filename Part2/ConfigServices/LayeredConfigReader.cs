using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    class LayeredConfigReader : IConfigReader
    {
        //注入多个ConfigServices
        private readonly IEnumerable<IConfigService> services;
        public LayeredConfigReader(IEnumerable<IConfigService> services)
        {
            this.services = services;
        }
        public string GetValue(string name)
        {
            string value = null;
            foreach (var item in services)
            {
                string newValue = item.GetValue(name);
                if (newValue != null)
                {
                    value = newValue;//最后一个不为null的值，就是最终值
                }

            }
            return value;
        }
    }
}
