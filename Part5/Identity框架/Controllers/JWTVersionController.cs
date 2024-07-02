using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity框架.Controllers
{
    /// <summary>
    /// JWT无法被提前撤回问题
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JWTVersionController : ControllerBase
    {
        private readonly IOptionsSnapshot<JWTSettings> optionsSnapshot;
        private readonly MyDbContext myDbContext;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<MyRole> roleManager;
        public JWTVersionController(IOptionsSnapshot<JWTSettings> optionsSnapshot, MyDbContext myDbContext, IWebHostEnvironment hostEnvironment, UserManager<MyUser> userManager, RoleManager<MyRole> roleManager)
        {
            this.optionsSnapshot = optionsSnapshot;
            this.myDbContext = myDbContext;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string BuildToken(List<Claim> claims)
        {
            DateTime expires = DateTime.Now.AddSeconds(optionsSnapshot.Value.ExpireSeconds);
            byte[] secBytes = Encoding.UTF8.GetBytes(optionsSnapshot.Value.SecKey);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims,
                expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }
        [HttpPost]
        [NotCheckJWTVersion]
        public async Task<ActionResult> Login(CheckPwdRequest req)
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
                user.JWTVersion++;//!!
                await userManager.UpdateAsync(user);//!!
                //生成Token
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim("JWTVersion", user.JWTVersion.ToString()));
                var roles = await userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                string token = BuildToken(claims);
                return Ok(token);
            }
            else
            {
                //添加锁定次数，达到一定次数的时候就会被锁定 可以在初始化中配置
                await userManager.AccessFailedAsync(user);
                return BadRequest("用户名或者密码错误!");
            }
        }
    }
}
