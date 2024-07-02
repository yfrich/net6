using System;
using System.IO;

namespace TestLib1
{
    public class Class1
    {
        public static void Test()
        {
            Console.WriteLine(typeof(FileStream).Assembly.Location);
            Console.WriteLine(typeof(Class1).Assembly.Location);
        }
    }
}
