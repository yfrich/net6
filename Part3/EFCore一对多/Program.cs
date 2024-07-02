using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore一对多
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                var a = db.Articles.Where(t => t.Id == 9).Single();//select
                a.Price = 99;//update
                await db.SaveChangesAsync();
                //Update t set price=99 where id=9;
                */

                /*
                //直接生成Update 语句 ，不Select
                Article a = new Article { Id = 6, Price = 99 };
                db.Entry(a).Property(t => t.Price).IsModified = true;
                Console.WriteLine(db.Entry(a).DebugView.LongView);
                await db.SaveChangesAsync();
                */
                /*
                //删除
                Article a = new Article { Id = 24582 };
                db.Entry(a).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                */


                //批量操作
                //新增
                /*
                Article a1 = new Article { Message = "xxx", Price = 9, Title = "asdasda" };
                Article a2 = new Article { Message = "xxx", Price = 9, Title = "asdasda" };
                Article a3 = new Article { Message = "xxx", Price = 9, Title = "asdasda" };

                await db.Articles.AddRangeAsync(a1, a2, a3);
                */
                //修改
                /*
                foreach (var item in db.Articles)
                {
                    item.Price = item.Price + 1;
                }
                */

                /*
                //删除
                db.RemoveRange(db.Articles.Where(t => t.Id > 24583));
                await db.SaveChangesAsync();

                */
                //ZACK 批量删除
                /*
                int a = await db.DeleteRangeAsync<Article>(t => t.Id > 20 || t.Title.Contains("X"));
                //db.Articles.DeleteRangeAsync(db,t=>t.)

                */
                /*
                //ZACK 批量更新
                int a = await db.BatchUpdate<Article>()
                     .Set(a => a.Price, a => 5.0)
                     .Set(t => t.Title, t => t.Title + DateTime.Now.Second)
                     .Where(t => t.Title == t.Message.Substring(0, 2))
                     .ExecuteAsync();
                //批量新增
                //SqlBulkCopy
                */

                //全局查询筛选器
                /*
                var a = db.Articles.Single(t => t.Id == 2);
                a.IsDeleted = true;
                await db.SaveChangesAsync();
                */

                /*
                //禁用 及性能问题
                var items = db.Articles.IgnoreQueryFilters().Where(t => t.Price > 10);
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id},{item.Title}");
                }

                */


            }
        }
        static async Task MainTracking(string[] args)
        {
            /*
            using (MyDbContext db = new MyDbContext())
            {
                //只要一个实体对象和DBContext发生任何的关系(查询，Add，与DbContext有关系的其他对象产生关系)都默认会被DbContext跟踪。
                Article a = await db.Articles.FirstAsync();
                a.Price = 999;//第一次执行
                a.Title = "[突发]" + a.Title + "";//第二次执行 不修改价格的话 是不会进行改变Price的SQL

                //Article a1 = new Article();
                //db.Articles.Add(a1);//Added

                //db.Articles.Remove(a);
                await db.SaveChangesAsync();
            }
            */

            //EntityEntry
            using (MyDbContext db = new MyDbContext())
            {
                /*
                var items = db.Articles.Take(3).ToArray();
                var a1 = items[0];
                var a2 = items[1];
                var a3 = items[2];

                var a4 = new Article { Title = "Add", Message = "xxx" };
                var a5 = new Article { Title = "Aasdasd", Message = "12312312" };
                a1.Price += 1;
                db.Remove(a2);
                db.Articles.Add(a4);

                EntityEntry e1 = db.Entry(a1);
                EntityEntry e2 = db.Entry(a2);
                EntityEntry e3 = db.Entry(a3);
                EntityEntry e4 = db.Entry(a4);
                EntityEntry e5 = db.Entry(a5);
                Console.WriteLine($"{e1.State}");
                Console.WriteLine($"{e1.DebugView.LongView}");
                Console.WriteLine($"{e2.State}");
                Console.WriteLine($"{e3.State}");
                Console.WriteLine($"{e4.State}");
                Console.WriteLine($"{e5.State}");
                */

                //AsNoTracking
                //var items = db.Articles.Take(3).ToArray();
                var items = db.Articles.AsNoTracking().Take(3).ToArray();
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Message}");
                }
                var a1 = items[0];
                a1.Price += 1;
                Console.WriteLine(db.Entry(a1).State);
                await db.SaveChangesAsync();
            }

        }
        static async Task MainFSQL(string[] args)
        {
            int age = 12;
            string name = ";delet T_Articles;";
            /*
            string sql = @$"insert into T_Articles (Title,Message,Price)
                select Title,{name},Price from T_Articles where Price>={age}
                ";
            Console.WriteLine(sql);
            */
            /*
            FormattableString sql = @$"insert into T_Articles (Title,Message,Price)
                select Title,{name},Price from T_Articles where Price>={age}
                ";
            Console.WriteLine(sql.Format);
            Console.WriteLine(string.Join(',', sql.GetArguments()));
            */
            /*
            //string s = "我是的名字" + name + ",我的年龄是" + age;
            //内插值语法
            string s = $"我的名字是{name},我的年龄是{age}";
            Console.WriteLine(s);
            */
            using (MyDbContext db = new MyDbContext())
            {
                /*
                //执行非查询原生SQL语句
                await db.Database.ExecuteSqlInterpolatedAsync(@$"insert into T_Articles (Title,Message,Price)
                select Title,{name},Price from T_Articles where Price>={age}
                ");
                */
                

                /*
                //执行实体查询
                //原生SQL like 语句 的匹配语法由值来体现而不是在SQL里面提现
                string titileParttern = "%我%";
                //var quertable = db.Articles.FromSqlInterpolated(@$"select *from T_Articles t where Title like {titileParttern} order by newid()");
                var quertable = db.Articles.FromSqlInterpolated(@$"select *from T_Articles t where Title like {titileParttern}  ");

                foreach (var item in quertable.Include(t => t.Comments).OrderBy(t => Guid.NewGuid()).Skip(3).Take(2))
                {
                    Console.WriteLine(item.Title);
                    item.Title = "XXXX";
                    foreach (var c in item.Comments)
                    {
                        Console.WriteLine(c.Message);
                    }
                }
                db.SaveChanges();
                */

                /*
                //执行纯原生SQL语句 跳过了EF Core 复杂的SQL语句可以使用 Dapper等框架。
                DbConnection conn = db.Database.GetDbConnection();//拿到Context对应底层的Connection
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select Price,Count(*) from T_Articles group by Price";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            double price = reader.GetDouble(0);
                            int count = reader.GetInt32(1);
                            Console.WriteLine($"{price},{count}");
                        }
                    }
                }
                */
                //Dapper执行
                var items = db.Database.GetDbConnection().Query<GroupArticleByPrice>("select Price,Count(*)PCount from T_Articles group by Price");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Price},{item.PCount}");
                }

            }
        }
        static async Task MainAsync(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                int c = await db.Articles.CountAsync();
                Console.WriteLine(c);
                */

                /*
                Article a = new Article { Title = "老于炸了", Message = "老于飞上天了" };
                await db.Articles.AddAsync(a);
                await db.SaveChangesAsync();
                var a1 = await db.Articles.FirstAsync();
                Console.WriteLine(a1.Title);

                */
                //循环遍历的异步问题
                //方式1
                /*
                foreach (var item in await db.Articles.ToListAsync())
                {
                    Console.WriteLine(item.Title);
                }
                */
                //方式2 
                await foreach (var item in db.Articles.AsAsyncEnumerable())
                {
                    Console.WriteLine(item.Title);
                }
            }
        }
        static void Main1(string[] args)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //关系配置
                //新增学习
                /*
                Article a1 = new Article { Title = "我是文章1", Message = "我是哈哈哈哈哈" };

                Comment c1 = new Comment { Message = "太牛了" };
                //也不需要在多对象里面赋值。编写没问题
                //Comment c1 = new Comment { Message = "太牛了", TheArticle=a1};
                Comment c2 = new Comment { Message = "吹吧" };

                a1.Comments.Add(c1);
                a1.Comments.Add(c2);
                //只要建立了一个关系 就会顺着干爬 找到所有的相应关联数据。
                ctx.Articles.Add(a1);
                //不需要加 只要在主表加了一样 编写没问题。
                //ctx.Comments.Add(c1);

                ctx.SaveChanges();

                */

                //获取关系数据 获取主表信息 及附加子表信息
                //Article a = ctx.Articles.Single(t => t.Id == 2);
                /*
                Article a = ctx.Articles.Include(t => t.Comments).Single(t => t.Id == 2);
                Console.WriteLine($"title:{a.Title};message:{a.Message}");
                foreach (var item in a.Comments)
                {
                    Console.WriteLine(item.Message);
                }
                */
                //获取子表信息 及对应的主表信息
                /*
                //Comment cmt = ctx.Comments.Single(t => t.Id == 3);
                Comment cmt = ctx.Comments.Include(t => t.TheArticle).Single(t => t.Id == 3);
                Console.WriteLine(cmt.Message);
                Console.WriteLine(cmt.TheArticle.Message);
                */

                //需求 添加额外外键，我只想要文章的ID 不想要文章的内容
                /*
                //方式1 会把不需要的字段也返回。 不利于数据库优化
                Comment cmt = ctx.Comments.Include(t => t.TheArticle).Single(t => t.Id == 3);
                Console.WriteLine($"{cmt.Id},{cmt.TheArticle.Id}");
                */
                //var article = ctx.Articles.Single(t => t.Id == 2);
                //单个实体生成指定列的SQL,先投影在获取
                //var article1 = ctx.Articles.Select(t => new { t.Id, t.Title }).First();
                //Console.WriteLine($"{article1.Id},{article1.Title}");

                //获取多关系实体的指定列获取
                /*
                var commont1 = ctx.Comments.Select(t => new { id = t.Id, AId = t.TheArticle.Id }).Single(t => t.id == 3);
                Console.WriteLine($"{commont1.id},{commont1.AId}");

                */

                /*
                //只想查询指定列，不用JOIN查询 
                //定义了额外的外键，使用的话要直接用，而不能关联到一的实体进行获取。
                var commont1 = ctx.Comments.Select(t => new { id = t.Id, AId = t.TheArticleId }).Single(t => t.id == 3);
                Console.WriteLine($"{commont1.id},{commont1.AId}");
                //var commont1 = ctx.Comments.Single(t => t.Id == 3);
                //Console.WriteLine($"{commont1.Id},{commont1.TheArticleId}");
                */

                //单项导航查询
                //查询所有离职单信息及对应的审核人
                /*
                var l = ctx.Leaves.FirstOrDefault();
                if (l != null)
                {
                    Console.WriteLine(l.Remarks);
                }
                */
                /*
                //插入数据
                User u1 = new User { Name = "我是于总" };
                //User u2 = new User { Name = "萧克孜" };
                Leave l1 = new Leave { Remarks = "回家拆迁", Requester = u1 };
                ctx.Leaves.Add(l1);
                ctx.SaveChanges();

                */

                /*
                //配置关系在哪里配置都可以
                var a = ctx.Articles.Include(t => t.Comments).First();
                Console.WriteLine(a.Title);
                foreach (var item in a.Comments)
                {
                    Console.WriteLine(item.Message);
                }
                */

                //查询评论中含有"微软"的文章

                //例 如果有性能瓶颈，则 可以对比方式1 方式2
                /*
                //方式1
                var test1 = ctx.Articles.Where(t => t.Comments.Any(s => s.Message.Contains("微软")));
                foreach (var item in test1)
                {
                    Console.WriteLine($"{item.Title},{item.Message}");
                }
                */

                /*
                //方式2
                var test1 = ctx.Comments.Where(t => t.Message.Contains("微软")).Select(t => t.TheArticle).Distinct();
                foreach (var item in test1)
                {
                    Console.WriteLine($"{item.Title},{item.Message}");
                }
                */

                //探索 IEnumerable 与 IQueryable

                //List<Comment> list = new List<Comment>();
                //list.Where(t => t.Message.Contains("微软"));
                //IQueryable<Comment> test1 = ctx.Comments.Where(t => t.Message.Contains("微软"));
                //IEnumerable<Comment> comments = ctx.Comments;

                IQueryable<Comment> comments = ctx.Comments;
                IEnumerable<Comment> test1 = ctx.Comments.Where(t => t.Message.Contains("微软"));
                foreach (var item in test1)
                {
                    Console.WriteLine(item.Message);
                }

                //客户端评估的应用场景。
                /*
                var cmts = ctx.Comments.Select(t => new { Id = t.Id, Pre = t.Message.Substring(0, 2) + "..." });
                foreach (var item in cmts)
                {
                    Console.WriteLine($"{item.Id},{item.Pre}");
                }
                */
                /* 
                //数据库执行较慢的情况下
                var cmts = ((IEnumerable<Comment>)ctx.Comments).Select(t => new { Id = t.Id, Pre = t.Message.Substring(0, 2) + "..." });
                foreach (var item in cmts)
                {
                    Console.WriteLine($"{item.Id},{item.Pre}");
                }
                */
                /* 编码无法被EF翻译的情况
                //var cmts = ctx.Comments.Where(t => IsOK(t.Message)).Select(t => new { Id = t.Id, Pre = t.Message.Substring(0, 2) + "..." });
                var cmts = ((IEnumerable<Comment>)ctx.Comments).Where(t => IsOK(t.Message)).Select(t => new { Id = t.Id, Pre = t.Message.Substring(0, 2) + "..." });
                foreach (var item in cmts)
                {
                    Console.WriteLine($"{item.Id},{item.Pre}");
                }
                */

                /*
                Console.WriteLine("准备Where");
                IQueryable<Article> articles = ctx.Articles.Where(t => t.Title.Contains("微软"));
                Console.WriteLine("准备Foreach");
                //foreach (var item in articles)
                //{
                //    Console.WriteLine(item.Title);
                //}
                Console.WriteLine(articles.Count());
                Console.WriteLine(articles.Max(t => t.Id));
                Console.WriteLine("完成Foreach");

                //foreach (var item in articles)
                //{
                //    Console.WriteLine(item.Title);
                //}
                */
                /*
                IQueryable<Article> arts = ctx.Articles.Where(t => t.Id > 1);
                IQueryable<Article> arts2 = arts.Skip(2);
                IQueryable<Article> arts3 = arts2.Take(2);
                IQueryable<Article> arts4 = arts3.Where(t=>t.Title.Contains("微软"));
                arts4.ToArray();
                */

                //分布构建
                //QueryArticles("微软", true, true, 80);
                //QueryArticles("微软", false, false, 80);

                /*
                IQueryable<Article> arts = ctx.Articles.Where(t => t.Id > 0);
                Console.WriteLine(arts.Count());
                Console.WriteLine(arts.LongCount());
                //Console.WriteLine(arts.Max(t => t.Price));
                //IQueryable<Article> arts2 = arts.Where(t => t.Title.Contains("微软"));
                //arts2.ToList();
                */

                //分页查询

                //PrintPage(2, 3);

                //了解IQueryable
                //DataReader方式 断开数据库连接 会发现连接断开
                /*
                foreach (var item in ctx.Articles)
                {
                    Console.WriteLine(item.Title);
                    Thread.Sleep(10);
                }
                */

                /*
                //方法需要返回结果，并且在方法内销毁了DBContext，是不能返回IQueryable
                var items = QueryNotBZY();
                foreach (var item in items)
                {
                    Console.WriteLine(item.Title);
                }
                */
                /*
                foreach (var a in ctx.Articles.ToArray())
                {
                    Console.WriteLine(a.Title);
                    foreach (var b in ctx.Comments.ToArray())
                    {
                        Console.WriteLine(b.Message);
                    }
                }
                */

            }
        }
        static IEnumerable<Article> QueryNotBZY()
        {
            using (MyDbContext db = new MyDbContext())
            //MyDbContext db = new MyDbContext();
            {
                return db.Articles.Where(t => t.Title.Contains("微软")).ToList();
            }
        }
        /// <summary>
        /// 打印某一页的数据及总页数
        /// </summary>
        /// <param name="pageIndex">页码(从1开始)</param>
        /// <param name="pageSize">每页数据条数</param>
        static void PrintPage(int pageIndex, int pageSize)
        {
            using (MyDbContext db = new MyDbContext())
            {
                IQueryable<Article> arts = db.Articles.Where(t => t.Id >= 3);
                var items = arts.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                foreach (var item in items)
                {
                    Console.WriteLine(item.Title);
                }
                long count = arts.LongCount();
                var pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                Console.WriteLine($"总页数：{pageCount}");
            }
        }
        static void QueryArticles(string searchWords, bool searchAll, bool orderByPrice, double upperPrice)
        {
            using (MyDbContext db = new MyDbContext())
            {
                IQueryable<Article> arts = db.Articles.Where(t => t.Price <= upperPrice);
                if (searchAll)
                {
                    arts = arts.Where(t => t.Title.Contains(searchWords) || t.Title.Contains(searchWords));
                }
                else
                {
                    arts = arts.Where(t => t.Title.Contains(searchWords));
                }
                if (orderByPrice)
                {
                    arts = arts.OrderBy(t => t.Price);
                }
                foreach (var item in arts)
                {
                    Console.WriteLine(item.Title);
                }
            }
        }
        static bool IsOK(string s)
        {
            if (s.StartsWith("a"))
            {
                return s.Length > 5;
            }
            else
            {
                return s.Length < 3;
            }
        }
    }
}
