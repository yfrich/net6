using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    public class EnVarConfigService : IConfigService
    {
        public string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
