using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Domain;
using Users.Infrastructure;

namespace Users.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDomainService userService;

        public LoginController(UserDomainService userDomainService)
        {
            this.userService = userDomainService;
        }

        [HttpPost]
        [UnitOfWork(typeof(UserDbContext))]//因为CheckPassword可能有修改数据的操作
        public async Task<IActionResult> LoginByPhoneAndPassword(LoginByPhoneAndPwdRequest req)
        {
            if (req.Password.Length <= 3)
            {
                //体现数据校验是应用层处理的
                return BadRequest("密码长度必须大于3");
            }
            var result = await userService.CheckPassword(req.PhoneNumber, req.Password);
            switch (result)
            {
                case UserAccessResult.OK:
                    return Ok("登陆成功");
                case UserAccessResult.PhoneNumberNotFound:
                case UserAccessResult.NoPassword:
                case UserAccessResult.PasswordError:
                    return BadRequest("登录失败，手机或密码错误");
                case UserAccessResult.Lockout:
                    return BadRequest("账户被锁定");
                default:
                    throw new ApplicationException($"未知值{result}");
            }
        }
    }
}
