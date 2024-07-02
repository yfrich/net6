using ASPNETCOREWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using System.Text.Json;
using Zack.ASPNETCore;

namespace ASPNETCOREWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<TestController> logger;
        private readonly IMemoryCacheHelper memoryCacheHelper;
        private readonly IDistributedCache distributedCache;
        private readonly IDistributedCacheHelper distributedCacheHelper;
        public TestController(IMemoryCache cache, ILogger<TestController> logger, IMemoryCacheHelper memoryCacheHelper, IDistributedCache distributedCache, IDistributedCacheHelper distributedCacheHelper)
        {
            this.cache = cache;
            this.logger = logger;
            this.memoryCacheHelper = memoryCacheHelper;
            this.distributedCache = distributedCache;
            this.distributedCacheHelper = distributedCacheHelper;
        }
        [HttpGet]
        public Person GetPerson()
        {
            return new Person("RichFu", 18);
        }
        [HttpPost]
        public string[] SaveNote(SaveNoteRequest req)
        {
            System.IO.File.WriteAllText($"{req.Title}.txt", req.Content);
            return new string[] { "ok", req.Title };
        }
        /// <summary>
        ///  客户端响应缓存 Duration缓存秒数
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 20)]
        [HttpGet]
        public DateTime NowA()
        {
            return DateTime.Now;
        }
        [HttpGet]
        public async Task<ActionResult<Book?>> GetBookById(long id)
        {
            logger.LogInformation($"开始执行GetBookById,Id={id}");
            //GetOrCreateAsync 二合一：1）从缓存取数据 2）从数据源取数据并返回给调用者及保存到缓存
            Book? result = await cache.GetOrCreateAsync($"Book{id}", async (t) =>
            {
                logger.LogInformation($"缓存未找到，到数据库查一查,Id={id}");
                //设置绝对缓存有效时间10秒
                //t.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                //设置滑动缓存有效时间10秒
                //t.SlidingExpiration = TimeSpan.FromSeconds(10);

                //两种方式混用
                //t.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                //t.SlidingExpiration = TimeSpan.FromSeconds(10);

                //return await MyDbContext.GetByIdAsync(id);

                //缓存穿透解决方案GetOrCreateAsync 把空的数据也存储到缓存上
                //缓存雪崩问题，基础时间上添加一个随机时间
                Book? d = await MyDbContext.GetByIdAsync(id);
                t.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5 + Random.Shared.Next(10, 15));
                logger.LogInformation($"数据库中查询的结果是：" + (d == null ? "null" : d.ToString()));
                return d;
            });
            logger.LogInformation($"GetOrCreateAsync结果是{result}");
            if (result == null)
            {
                return NotFound($"找不到id={id}的书籍");
            }
            else
            {
                return result;
            }
        }
        /// <summary>
        /// 内存缓存Helper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Book?>> Test2(long id)
        {
            var b = await memoryCacheHelper.GetOrCreateAsync("Book" + id, async (t) =>
            {
                return await MyDbContext.GetByIdAsync(id);
            });
            if (b == null)
            {
                return NotFound($"找不到id={id}的书籍");
            }
            else
            {
                return b;
            }
        }

        /// <summary>
        /// 分布式缓存实现
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Book?>> Test3(long id)
        {
            Book? book;
            string? s = await distributedCache.GetStringAsync("Book" + id);
            if (s == null)
            {
                //数据库查询
                book = await MyDbContext.GetByIdAsync(id);
                //将缓存数据序列化成字符串放入缓存中
                await distributedCache.SetStringAsync("Book" + id, JsonSerializer.Serialize(book));
            }
            else
            {
                //反序列换字符串
                book = JsonSerializer.Deserialize<Book?>(s);
            }

            if (book == null)
            {
                return NotFound($"找不到id={id}的书籍");
            }
            else
            {
                return book;
            }
        }
        /// <summary>
        /// 分布式缓存帮助类实现
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Book?>> Test4(long id)
        {
            var book = await distributedCacheHelper.GetOrCreateAsync("Book" + id, async t =>
            {
                //设置滑动过期及更详细的配置。
                t.SlidingExpiration = TimeSpan.FromSeconds(Random.Shared.NextDouble(10.0, 20.0));
                var book = await MyDbContext.GetByIdAsync(id);
                return book;
            }, 20);

            if (book == null)
            {
                return NotFound($"找不到id={id}的书籍");
            }
            else
            {
                return book;
            }
        }
    }
}
