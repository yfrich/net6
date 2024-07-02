using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 配置系统1
{
    class FxConfigProvider : FileConfigurationProvider
    {
        public FxConfigProvider(FxConfigSource source) : base(source)
        {

        }
        public override void Load(Stream stream)
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);
            var csNodes = xmlDoc.SelectNodes("/configuration/connectionStrings/add");
            foreach (XmlNode item in csNodes.Cast<XmlNode>())
            {
                string name = item.Attributes["name"].Value;
                string connectionString = item.Attributes["connectionString"].Value;

                //[conn1:{connectionString:"asd",providername:"mysql"},
                //conn2:{connectionString:"asdasd",providername:"mysql"},}]
                data[$"{name}:connectionString"] = connectionString;
                var attProviderName = item.Attributes["providerName"];
                if (attProviderName != null)
                {
                    data[$"{name}:providerName"] = attProviderName.Value;
                }
            }

            var asNodes = xmlDoc.SelectNodes("/configuration/appSettings/add");
            foreach (XmlNode item in asNodes.Cast<XmlNode>())
            {
                string key = item.Attributes["key"].Value;
                key.Replace(".", ":");
                string value = item.Attributes["value"].Value;
                data[key] = value;
            }
            this.Data = data;
        }
    }
}
