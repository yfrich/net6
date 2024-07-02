using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity框架.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]//此方式说明控制器下的所有方法都需要登录才能访问
    public class UseJWTController : ControllerBase
    {
        [HttpGet]
        public string Test1()
        {
            var claim = this.User.FindFirst(ClaimTypes.Name);

            return $"ok+{claim.Value}";
        }
        [HttpGet]
        [AllowAnonymous]//此标记表示可以跳过认证
        public string Test2()
        {

            return $"666";
        }

        [HttpGet]
        [Authorize(Roles = "admin")]//对于角色进行授权
        public string Test3()
        {
            return "888";
        }
    }
}
