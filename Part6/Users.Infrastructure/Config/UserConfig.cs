using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Infrastructure.Config
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");
            //配置值对象（从属）
            builder.OwnsOne(t => t.PhoneNumber, nb =>
            {
                nb.Property(b => b.Number).HasMaxLength(20).IsUnicode(false);
            });
            //一对一
            builder.HasOne(t => t.UserAccessFail).WithOne(t => t.User).HasForeignKey<UserAccessFail>(t => t.UserId);
            //配置成员变量
            builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
        }
    }
}
