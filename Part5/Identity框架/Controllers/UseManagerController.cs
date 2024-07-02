using Identity框架.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Identity框架.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UseManagerController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<MyRole> roleManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IOptionsSnapshot<JWTSettings> optionsSnapshot;

        private const string key = "asdasfqweo*)()^&*^&*^asdkd1kj123lkj_";

        public UseManagerController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager, IWebHostEnvironment hostEnvironment, IOptionsSnapshot<JWTSettings> optionsSnapshot)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.hostEnvironment = hostEnvironment;
            this.optionsSnapshot = optionsSnapshot;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Test1()
        {
            if (await roleManager.RoleExistsAsync("admin") == false)
            {
                MyRole role = new MyRole { Name = "admin" };
                var idResult = await roleManager.CreateAsync(role);
                if (!idResult.Succeeded)
                {
                    return BadRequest("CreateAsync failed");
                    //idResult.Errors  错误信息 可操作进行记录到日志中
                }
            }
            MyUser user = await userManager.FindByNameAsync("rich");
            if (user == null)
            {
                MyUser myUser = new MyUser { UserName = "rich" };
                var idResult = await userManager.CreateAsync(myUser, "123456");
                if (!idResult.Succeeded)
                {
                    return BadRequest("CreateAsync failed");
                }
                idResult = await userManager.AddToRoleAsync(myUser, "admin");
            }
            else
            {
                if (await userManager.IsInRoleAsync(user, "admin") == false)
                {
                    var idResult = await userManager.AddToRoleAsync(user, "admin");
                    if (!idResult.Succeeded)
                    {
                        return BadRequest("CreateAsync failed");
                    }
                }
            }
            return "ok";
        }
        /// <summary>
        /// 检查登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CheckUserPwd(CheckPwdRequest req)
        {
            string userName = req.UserName;
            string pwd = req.Password;
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                //根据环境 可根据此方式进行判定
                if (hostEnvironment.IsDevelopment())
                {
                    return BadRequest("用户名不存在");
                }
                else
                {
                    return BadRequest();//更安全
                }
            }
            if (await userManager.IsLockedOutAsync(user))
            {
                return BadRequest($"用户被锁定，锁定结束时间：{user.LockoutEnd}");
            }
            if (await userManager.CheckPasswordAsync(user, pwd))
            {
                //重置锁定次数
                await userManager.ResetAccessFailedCountAsync(user);
                return Ok("登陆成功");
            }
            else
            {
                //添加锁定次数，达到一定次数的时候就会被锁定 可以在初始化中配置
                await userManager.AccessFailedAsync(user);
                return BadRequest("用户名或者密码错误!");
            }
        }

        /// <summary>
        /// 发送重置密码的验证码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendResetPasswordToken(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("用户名不存在！");
            }
            //发送验证码 验证码是存储在缓存中的，重启项目后无法使用之前发送的。
            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine($"用户修改密码的验证码：{token}");
            return Ok("验证码已发送！");
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> ResetPassword(string userName, string token, string newPassword)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("用户名不存在！");
            }
            var idResult = await userManager.ResetPasswordAsync(user, token, newPassword);
            if (idResult.Succeeded)
            {
                await userManager.ResetAccessFailedCountAsync(user);
                return Ok("密码重置成功");
            }
            else
            {
                await userManager.AccessFailedAsync(user);
                return BadRequest($"错误原因：{string.Join(',', idResult.Errors.Select(t => $"{t.Code}||{t.Description}"))}");
            }
        }
        /// <summary>
        /// 加密生成JWT令牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateJWT()
        {
            //Claim  一个就代表一个用户信息
            var claims = new List<Claim>
            {
                //ClaimTypes 会通用，如果自定义的话可能会被其他系统不认。尽量不用
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "rich"),
                new Claim(ClaimTypes.HomePhone, "999"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "manager"),
                new Claim("Passport", "123456"),
                new Claim("Passport", "123456")
            };
            //密钥
            DateTime expires = DateTime.Now.AddHours(1);
            //asdadqwlkejqwlejqwlkejqlwekj123124142 篡改测试用
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims,
                expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return Ok(jwt);
        }
        /// <summary>
        /// 解密不校验
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        [HttpPost]
        [NotCheckJWTVersion]
        public ActionResult DecodeJWT([FromBody] string jwt)
        {
            //读取不校验签名

            string[] segments = jwt.Split('.');
            string head = JwtDecode(segments[0]);
            string payload = JwtDecode(segments[1]);
            Console.WriteLine("--------head--------");
            Console.WriteLine(head);
            Console.WriteLine("--------payload--------");
            Console.WriteLine(payload);
            string JwtDecode(string s)
            {
                s = s.Replace('-', '+').Replace('_', '/');
                switch (s.Length % 4)
                {
                    case 2:
                        s += "==";
                        break;
                    case 3:
                        s += "=";
                        break;
                }
                var bytes = Convert.FromBase64String(s);
                return Encoding.UTF8.GetString(bytes);
            }

            return Ok("解析完成");
        }
        /// <summary>
        /// 解密并且校验
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DecodeJWT2([FromBody] string jwt)
        {
            //校验签名
            //调用JwtSecurityTokenHandler类对JWT令牌进行解码
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                TokenValidationParameters valParam = new();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                valParam.IssuerSigningKey = securityKey;
                valParam.ValidateIssuer = false;
                valParam.ValidateAudience = false;
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt,
                        valParam, out SecurityToken secToken);
                foreach (var claim in claimsPrincipal.Claims)
                {
                    Console.WriteLine($"{claim.Type}={claim.Value}");
                }
                return Ok("解析完成");
            }
            catch (SecurityTokenSignatureKeyNotFoundException ex)
            {
                return Ok("异常Token");

            }
        }
        /// <summary>
        /// JWT 在Web API 中模拟登录使用
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Login(string userName, string password)
        {
            if (userName == "rich" && password == "123456")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
                claims.Add(new Claim(ClaimTypes.Name, "rich"));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                string jwt = BuildToken(claims);
                return Ok(jwt);
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string BuildToken(List<Claim> claims)
        {
            DateTime expires = DateTime.Now.AddSeconds(optionsSnapshot.Value.ExpireSeconds);
            //asdadqwlkejqwlejqwlkejqlwekj123124142 篡改测试用
            byte[] secBytes = Encoding.UTF8.GetBytes(optionsSnapshot.Value.SecKey);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims,
                expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }
    }
}
