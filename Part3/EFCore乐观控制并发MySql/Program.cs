using EFCore悲观并发MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore乐观控制并发MySql
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //乐观 单列控制

            using (MyDbContext db = new MyDbContext())
            {
                Console.WriteLine("请输入您的名字");
                string name = Console.ReadLine();
                var h = await db.Houses.FirstAsync();
                if (!string.IsNullOrEmpty(h.Owner))
                {
                    if (h.Owner == name)
                    {
                        Console.WriteLine("房子已经被你抢到了");
                    }
                    else
                    {
                        Console.WriteLine($"房子已经被【{h.Owner}】占了");
                    }
                    Console.ReadLine();
                    return;
                }
                h.Owner = name;
                await Task.Delay(10000);
                try
                {
                    await db.SaveChangesAsync();
                    Console.WriteLine("恭喜你，抢到了");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("并发访问冲突");
                    var entr1 = ex.Entries.First();
                    string newValue = (await entr1.GetDatabaseValuesAsync()).GetValue<string>("Owner");
                    Console.WriteLine($"房子被{newValue}抢走了");
                }
                Console.ReadLine();
            }

            //乐观多列控制？


        }
    }
}
