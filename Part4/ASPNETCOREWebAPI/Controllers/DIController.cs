using ASPNETCOREWebAPI.Models;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCOREWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DIController : ControllerBase
    {
        private readonly Calculator calculator;
        //private readonly TestService testService;
        private readonly Class1 class1;
        public DIController(Calculator calculator, Class1 class1)
        {
            this.calculator = calculator;
            this.class1 = class1;
            //this.testService = testService;
        }
        [HttpGet]
        public string Add1()
        {
            return calculator.Add(2, 3) + class1.Hello();
        }
        [HttpGet]
        public int Test1([FromServices] TestService testService, int x)
        {
            return testService.Count + x;
        }

    }
}
