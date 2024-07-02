using ASPNETCOREWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCOREWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public Person2[] GetAll()
        {
            return new Person2[] { new Person2(1, "BZY", 18), new Person2(2, "TOM", 5) };
        }
        [HttpGet]
        public Person2? GetById(long id)
        {
            if (id == 1)
            {
                return new Person2(1, "BZY", 18);
            }
            else if (id == 2)
            {
                return new Person2(2, "TOM", 5);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public string AddNew(Person2 p)
        {
            return "新增完成";
        }
    }
}
