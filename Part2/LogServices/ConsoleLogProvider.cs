using System;
using System.Collections.Generic;
using System.Text;

namespace LogServices
{
    class ConsoleLogProvider : ILogProvider
    {
        public void LogInfo(string msg)
        {
            Console.WriteLine($"Info:{msg}");
        }

        public void LogError(string msg)
        {
            Console.WriteLine($"Error:{msg}");
        }
    }
}
