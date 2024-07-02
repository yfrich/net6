using ASPNETCOREWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCOREWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HelloController
    {
        [HttpGet]
        public int Add(int i, int j)
        {
            return i + j;
        }

        [HttpGet]
        public async Task<string> Add2()
        {
            string s = await File.ReadAllTextAsync("f:/1.txt");
            return s.Substring(0, 1);
        }

        [HttpGet]
        public Person2 Test2()
        {
            return new Person2(1, "RichFu", 18);
        }
        /// <summary>
        /// 占位符方式捕捉 REST URL捕捉
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <returns></returns>
        [HttpGet("{i1}/{i2}")]
        public int Multi(int i1, int i2)
        {
            return i1 * i2;
        }

        /// <summary>
        /// 若占位符的名字于参数名字不一致可以指定路由的参数
        /// </summary>
        /// <param name="schoolName"></param>
        /// <param name="classNum"></param>
        /// <returns></returns>
        [HttpGet("students/school/{schoolName}/class/{classNo}")]
        public Person GetStudents(string schoolName, [FromRoute(Name = "classNo")] int classNum)
        {
            return new Person($"{schoolName},{classNum}", 18);
        }
        /// <summary>
        /// 默认情况下 参数是直接从QueryString中取的
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpPost]
        public int Save(int i, [FromQuery(Name = "j")] int j)
        {
            return i + j;
        }

        [HttpPost]
        public string AddPerson1(Person p1)
        {
            return $"保存成功：{p1.Name},{p1.Age}";
        }

        /// <summary>
        /// Route、QueryString、Body参数获取混用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fid"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public string UpdatePerson1([FromRoute] int id, int fid, Person p1, [FromHeader(Name = "User-Agent")] string ua)
        {
            return $"更新成功：{p1.Name},{fid},{id},{ua}";
        }
    }

}
