using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Infrastructure.Config
{
    internal class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("T_UserLoginHistories");
            builder.OwnsOne(t => t.PhoneNumber, nb =>
            {
                nb.Property(b => b.Number).HasMaxLength(20).IsUnicode(false);
            });
        }
    }
}
