using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Identity框架.SignalR
{
    //[Authorize(Roles = "admin")]
    [Authorize]
    public class ChatRoomHub : Hub
    {
        private readonly UserManager<MyUser> userManager;

        public ChatRoomHub(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SendPublicMessage(string message)
        {
            var claim = this.Context.User.FindFirst(ClaimTypes.Name);

            string connId = this.Context.ConnectionId;
            string msg = $"{claim.Value}{DateTime.Now}:{message}";

            //this.Clients.Clients();//给指定的人发送 可以是多人
            //this.Clients.Caller//自己
            //this.Clients.Group("dev");组发送
            //this.Clients.Others//其他人
            //this.Clients.OthersInGroup();//某个组的其他人
            //this.Clients.User();//一个用户
            //this.Clients.Users();//多个用户
            //await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "dev");
            await Clients.All.SendAsync("ReceivePublicMessage", msg);
        }
        /// <summary>
        /// 私聊 一对一
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendPrivateMessage(string userName, string message)
        {
            var user = await userManager.FindByNameAsync(userName);
            long userId = user.Id;
            string currentUserName = this.Context.User.FindFirst(ClaimTypes.Name).Value;
            await this.Clients.User(userId.ToString()).SendAsync("PrivateMsgRecevied", currentUserName, message);

        }
    }
}
