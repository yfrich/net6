using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Identity框架.数据校验
{
    public class AddNewUserRequestValidator : AbstractValidator<AddNewUserRequest>
    {
        public AddNewUserRequestValidator(UserManager<MyUser> userManager)
        {
            RuleFor(t => t.Email).NotNull().EmailAddress().WithMessage("邮箱必须是合法的")
                .Must(t => t.EndsWith("@163.com") || t.EndsWith("@qq.com"))
                .WithMessage("只支持QQ和163邮箱")
                ;
            RuleFor(t => t.UserName).NotNull()
                .Length(3, 10).WithMessage("用户名长度3~10个长度")
                .MustAsync(async (t, _) =>
                {
                    //实现用户名重复校验
                    return await userManager.FindByNameAsync(t) == null;
                }).WithMessage(c => $"{c.UserName}用户名已存在！");
            RuleFor(t => t.Password).NotNull().Equal(t => t.Password2).WithMessage("两次密码不一致");
        }
    }
}
