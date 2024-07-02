using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_2
{
    internal class MyFile : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("MyFile Dispose");
        }
    }
}
