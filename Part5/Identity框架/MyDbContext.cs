using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity框架
{
    //需要指定泛型类 <MyUser, MyRole, long>
    //因为DBSet 是没指定在此处的。 
    public class MyDbContext : IdentityDbContext<MyUser, MyRole, long>
    {
        /// <summary>
        /// 支持ASP.NET 中进行注入 而不是在此类中进行注册
        /// 原来学习EFCore 使用重写 OnConfiguring 来初始化，现在改成使用以下方式在ASP.NET CORE中进行初始化
        /// </summary>
        /// <param name="options"></param>
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //一般不需要配置，直接用他的默认配置即可
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly, t =>
            //{
            //    return typeof(MyUserConfig) == t || typeof(MyRoleConfig) == t;
            //});
        }
    }
}
