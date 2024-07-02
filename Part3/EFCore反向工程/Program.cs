using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore反向工程
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (aspnetcorescaffoldContext db = new aspnetcorescaffoldContext())
            {
                /*
                Console.WriteLine(db.TPersons.Count());
                TPerson p1 = new TPerson();
                p1.Name = "BZY";
                p1.BirthDay = new DateTime(2008, 8, 8);
                db.TPersons.Add(p1);
                await db.SaveChangesAsync();
                */
                var persons = db.TPersons.Where(t => t.Name.ToLower() == "Bzy");
                string sql = persons.ToQueryString();
                Console.WriteLine($"这就是我想要的：{sql}");
                /*
                foreach (var item in persons)
                {
                    Console.WriteLine(item.Name);
                }
                db.TPersons.Add(new TPerson { BirthDay = DateTime.Now, Name = "asdasd" });
                await db.SaveChangesAsync();
                Console.WriteLine(db.TPersons.Count());
                */
            }
        }
        static bool IsOk(string a)
        {
            return a.Contains("B");
        }
    }
}
