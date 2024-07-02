using System;
using System.Linq;

namespace EFCoreTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var persons = ctx.Persons.Where(t => t.BirthDay.Year == 1998 && t.Name.Substring(3) == "abc").Take(3);
                foreach (var p in persons)
                {
                    Console.WriteLine(p.Name);
                }
            }
        }
    }
}
