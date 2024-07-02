using Identity框架.SignalR;
using Identity框架.数据校验;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Identity框架.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        private readonly IHubContext<ChatRoomHub> hubContext;


        public ValidatorController(UserManager<MyUser> userManager, IHubContext<ChatRoomHub> hubContext)
        {
            this.userManager = userManager;
            this.hubContext = hubContext;
        }
        [HttpPost]
        [NotCheckJWTVersion]
        public async Task<ActionResult> AddUser(AddNewUserRequest req)
        {
            MyUser user = new MyUser { UserName = req.UserName, Email = req.Email };
            var idRequest = await userManager.CreateAsync(user, req.Password);
            if (idRequest.Succeeded)
            {
                await hubContext.Clients.All.SendAsync("ReceivePublicMessage", $"欢迎新来的:{req.UserName}");
                return Ok("添加成功");
            }
            else
            {
                return Ok("添加失败");
            }
        }
    }
}
