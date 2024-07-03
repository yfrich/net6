using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Users.WebAPI
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception != null)//只有Action执行成功，才自动调用SaveChanges
            {
                return;
            }
            else
            {
                var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
                if (actionDesc == null)
                {
                    return;
                }
                var uowAttr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
                if (uowAttr == null)
                {
                    return;
                }
                foreach (var dbCtxType in uowAttr.DbContextTypes)
                {
                    var dbCtx = context.HttpContext.RequestServices.GetService(dbCtxType) as DbContext;//管DI要DbContext实例
                    if (dbCtx != null)
                    {
                        await dbCtx.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
