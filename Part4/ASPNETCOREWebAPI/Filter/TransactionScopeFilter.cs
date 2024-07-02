using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace ASPNETCOREWebAPI.Filter
{
    /// <summary>
    /// 自动启用事务筛选器
    /// </summary>
    public class TransactionScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //是否包含该特性
            bool hasNotTransactionAttribute = false;
            //context.ActionDescriptor 当前被执行的Action方法的描述信息
            //context.ActionArguments 当前被执行的Action方法的参数信息
            //由于ActionDescriptor 不仅仅是为了给MVC、API用。所以是一个抽象类，具体使用是MVC还是什么需要自己进行显示转换
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                var actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
                //actionDesc.MethodInfo 当前的Action方法
                //根据action方法是否包含指定特性进行校验
                //方式一
                //hasNotTransactionAttribute = actionDesc.MethodInfo.IsDefined(typeof(NotTransactionAttribute));
                //方式二
                hasNotTransactionAttribute = actionDesc.MethodInfo.GetCustomAttributes(typeof(NotTransactionAttribute), false).Any();
            }
            if (hasNotTransactionAttribute)
            {
                await next();
                return;
            }
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await next();
            if (result.Exception == null)
            {
                //如果内部代码执行无异常则进行事务提交
                tx.Complete();
            }
        }
    }
}
