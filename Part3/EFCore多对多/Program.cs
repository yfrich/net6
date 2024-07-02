using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EFCore多对多
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                Student st1 = new Student();
                st1.Name = "学生1";
                Student st2 = new Student();
                st2.Name = "学生2";
                Student st3 = new Student();
                st3.Name = "学生3";

                Teacher t1 = new Teacher();
                t1.Name = "lili";
                Teacher t2 = new Teacher();
                t2.Name = "nana";
                Teacher t3 = new Teacher();
                t3.Name = "jack";

                st1.Teachers.Add(t1);
                st1.Teachers.Add(t2);

                st2.Teachers.Add(t2);
                st2.Teachers.Add(t3);

                st3.Teachers.Add(t1);
                st3.Teachers.Add(t2);
                st3.Teachers.Add(t3);

                db.Students.Add(st1);
                db.Students.Add(st2);
                db.Students.Add(st3);
                db.Teachers.Add(t1);
                db.Teachers.Add(t2);
                db.Teachers.Add(t3);

                await db.SaveChangesAsync();
                */

                var teachers = db.Teachers.Include(t => t.Students);
                foreach (var item in teachers)
                {
                    Console.WriteLine($"老师：{item.Name}");
                    foreach (var st in item.Students)
                    {
                        Console.WriteLine("\t" + st.Name);
                    }
                }
            }
        }
    }
}
