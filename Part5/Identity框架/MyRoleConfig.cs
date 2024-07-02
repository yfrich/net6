using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity框架
{
    public class MyRoleConfig : IEntityTypeConfiguration<MyRole>
    {
        public void Configure(EntityTypeBuilder<MyRole> builder)
        {
            builder.ToTable("T_MyRole");
        }
    }
}
