using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Security.Claims;

namespace Identity框架
{
    public class JWTVersionFilter : IAsyncActionFilter
    {
        private readonly UserManager<MyUser> userManager;

        public JWTVersionFilter(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor controllerAction = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerAction == null)
            {
                await next.Invoke();
                return;
            }

            if (controllerAction.MethodInfo.GetCustomAttributes(typeof(NotCheckJWTVersionAttribute), true).Any())
            {
                await next.Invoke();
                return;
            }

            var claimJWTVer = context.HttpContext.User.FindFirst("JWTVersion");
            if (claimJWTVer == null)
            {
                context.Result = new ObjectResult("payload中没有JWTVersion") { StatusCode = 400 };
                return;
            }
            var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            long jwtVerFromClient = Convert.ToInt64(claimJWTVer.Value);
            //可以把用户信息加缓存，不用每次都查，可以做一个5秒的缓存
            var user = await userManager.FindByIdAsync(claimUserId.Value);
            if (user == null)
            {
                context.Result = new ObjectResult("user找不到") { StatusCode = 400 };
                return;
            }
            if (user.JWTVersion > jwtVerFromClient)
            {
                context.Result = new ObjectResult("JWT已过时") { StatusCode = 400 };
                return;
            }

            await next.Invoke();
        }
    }
}
