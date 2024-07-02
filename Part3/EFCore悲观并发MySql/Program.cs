using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EFCore悲观并发MySql
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //并发版本
            /*
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
                    return;
                }
                h.Owner = name;
                await Task.Delay(10000);
                Console.WriteLine("恭喜你，抢到了");
                await db.SaveChangesAsync();
                Console.ReadLine();
            }
            */

            //悲观控制并发
            /*
            using (MyDbContext db = new MyDbContext())
            using (var ts = await db.Database.BeginTransactionAsync())//使用锁之前要开启事务
            {
                Console.WriteLine("请输入您的名字");
                string name = Console.ReadLine();
                //var h = await db.Houses.FirstAsync();
                Console.WriteLine($"{DateTime.Now}:准备for update");
                var h = await db.Houses.FromSqlInterpolated($"select *from T_Hourses where Id=1 for update").SingleAsync();
                Console.WriteLine($"{DateTime.Now}:完成for update");
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
                    //释放锁 是否在未执行保存的时候也要进行释放锁
                    await ts.CommitAsync();
                    Console.ReadLine();
                    return;
                }
                h.Owner = name;
                await Task.Delay(10000);
                Console.WriteLine("恭喜你，抢到了");
                await db.SaveChangesAsync();
                Console.WriteLine($"{DateTime.Now}:保存完成");
                //释放锁
                await ts.CommitAsync();
                Console.ReadLine();
            }
            */

            //乐观控制并发

        }
    }
}
