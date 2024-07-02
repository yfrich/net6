using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //MyDbContext 相当于逻辑上的数据库
            using (MyDbContext dbContext = new MyDbContext())
            {
                /*
                    //把对象加入Dogs这个逻辑上的表里面
                    dbContext.Dogs.Add(new Dog { Name = "Trump" });
                    //dbContext.SaveChanges();//实际修改同步

                    //dbContext.Books.Add(new Book { Title = "书", AuthorName = "zeze", PubTime = DateTime.Now, Price = 50 });
                */


                /*
                //数据插入
                var b1 = new Book
                {
                    AuthorName = "杨中科",
                    Title = "零基础趣学C语言",
                    Price = 59.8,
                    PubTime = new DateTime(2019, 3, 1)
                };
                var b2 = new Book
                {
                    AuthorName = "Robert Sedgewick",
                    Title = "算法(第4版)",
                    Price = 99,
                    PubTime = new DateTime(2012, 10, 1)
                };
                var b3 = new Book
                {
                    AuthorName = "吴军",
                    Title = "数学之美",
                    Price = 69,
                    PubTime = new DateTime(2020, 5, 1)
                };
                var b4 = new Book
                {
                    AuthorName = "杨中科",
                    Title = "程序员的SQL金典",
                    Price = 52,
                    PubTime = new DateTime(2008, 9, 1)
                };
                var b5 = new Book
                {
                    AuthorName = "吴军",
                    Title = "文明之光",
                    Price = 246,
                    PubTime = new DateTime(2017, 3, 1)
                };
                dbContext.Books.Add(b1);
                dbContext.Books.Add(b2);
                dbContext.Books.Add(b3);
                dbContext.Books.Add(b4);
                dbContext.Books.Add(b5);
                */


                //EFCore 会帮我们转换成SQL语句
                //LIQN查询练习
                /*
                var books = dbContext.Books.Where(t => t.Price > 80);

                foreach (var b in books)
                {
                    Console.WriteLine(b.Title);
                }
                var book = dbContext.Books.Single(t => t.Title == "零基础趣学C语言");
                Console.WriteLine(book.Title);
                */

                /*
                var books = dbContext.Books.Where(t => t.Price > 10).OrderBy(t => t.Price);
                foreach (var item in books)
                {
                    Console.WriteLine(item.Title);
                }
                */

                /*
                var books = dbContext.Books.GroupBy(t => t.AuthorName).Select(t => new { Name = t.Key, BooksCount = t.Count(), MaxPrice = t.Max(g => g.Price) });

                foreach (var item in books)
                {
                    Console.WriteLine($"{item.Name},{item.BooksCount},{item.MaxPrice}");
                }
                */

                /*
                //查询后进行修改和删除
                var b = dbContext.Books.Single(t => t.Title == "数学之美");
                b.AuthorName = "junwu";
                var c = dbContext.Dogs.Single(t => t.Id == 3);
                dbContext.Dogs.Remove(c);
                
                */

                //批量修改 此方式需要先查询，然后依次进行更新
                /*
                var books = dbContext.Books.Where(t => t.Price > 10);
                foreach (var b in books)
                {
                    b.Price = b.Price + 1;
                }
                */

                //获取新增实体的主键信息
                /*
                Dog dog = new Dog();
                dog.Name = "111";
                Console.WriteLine(dog.Id);
                dbContext.Dogs.Add(dog);
                //有异步就异步
                await dbContext.SaveChangesAsync();
                Console.WriteLine(dog.Id);

                */

                /*
                Guid g = Guid.NewGuid();
                Console.WriteLine(g);
                Console.WriteLine(g.ToString());
                */

                Rabbit r1 = new Rabbit();
                r1.Id = Guid.NewGuid();
                r1.Name = "bzy";
                Console.WriteLine(r1.Id);
                dbContext.Rabbits.Add(r1);
                Console.WriteLine(r1.Id);
                await dbContext.SaveChangesAsync();
                Console.WriteLine(r1.Id);

            }
        }

        static void Main1(string[] args)
        {
            TestFluentAPI t = new TestFluentAPI();
            t.SetName("as").SetName("dasda").SetName("ass");
            Console.WriteLine(t.Name);
        }
    }
    class TestFluentAPI
    {
        public string Name { get; set; }
        public TestFluentAPI SetName(string name)
        {
            this.Name = name;
            return this;
        }
    }
}
