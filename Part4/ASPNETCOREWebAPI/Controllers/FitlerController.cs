using ASPNETCOREWebAPI.Filter;
using EFCoreBooks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace ASPNETCOREWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FitlerController : ControllerBase
    {
        private readonly BookDbContext bookDbContext;
        private readonly PersonDbContext personDbContext;

        public FitlerController(BookDbContext bookDbContext, PersonDbContext personDbContext)
        {
            this.bookDbContext = bookDbContext;
            this.personDbContext = personDbContext;
        }

        /// <summary>
        /// 测试异常过滤器的方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test1()
        {
            string s = System.IO.File.ReadAllText("f:/123.txt");
            return s;
        }
        /// <summary>
        /// 测试方法过滤器的方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test2()
        {
            Console.WriteLine("Action执行中");
            return "HelloWord";
        }
        /// <summary>
        /// 案例实现自启用事务 不启用
        /// </summary>
        /// <returns></returns>
        [NotTransaction]
        [HttpPost]
        public async Task<string> Test3()
        {
            //新语法异步调用不用传参
            //using TransactionScope t = new TransactionScope();
            //异步方法需要传参
            using (TransactionScope t = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                personDbContext.Persons.Add(new Person { Age = 10, Name = "我是名字了" });
                await personDbContext.SaveChangesAsync();//一个事务
                bookDbContext.Books.Add(new Book { Titile = "我是哈哈哈", AuthorName = "我是作者啦", Price = 20, PubDate = DateTime.Now, TestMoreDB = "33" });
                await bookDbContext.SaveChangesAsync();//一个事务
                t.Complete();//主动提交
                return "ok";
            }
        }
        /// <summary>
        /// 案例实现自启用事务 启用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Test4()
        {
            personDbContext.Persons.Add(new Person { Age = 10, Name = "我是名字了" });
            await personDbContext.SaveChangesAsync();//一个事务
            bookDbContext.Books.Add(new Book { Titile = "我是哈哈哈", AuthorName = "我是作者啦", Price = 20, PubDate = DateTime.Now, TestMoreDB = "3ssss3" });
            await bookDbContext.SaveChangesAsync();//一个事务
            return "ok";
        }
    }
}
