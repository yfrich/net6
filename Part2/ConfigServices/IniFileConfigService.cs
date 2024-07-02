using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ConfigServices
{
    class IniFileConfigService : IConfigService
    {
        public string FilePath { get; set; }
        public string GetValue(string name)
        {
            var kv = File.ReadLines(FilePath).Select(t => t.Split('=')).Select(t => new { Name = t[0], Value = t[1] }).SingleOrDefault(t => t.Name == name);
            if (kv != null)
            {
                return kv.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
