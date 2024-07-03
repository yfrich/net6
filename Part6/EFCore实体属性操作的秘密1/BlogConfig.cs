using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.OwnsOne(t => t.Title, on =>
            {
                on.Property(e => e.Chinese).HasMaxLength(255);
                on.Property(e => e.English).HasColumnType("varchar(255)");
                //方式2
                //on.Property(e => e.Chinese).HasMaxLength(255).IsUnicode(true);
                //on.Property(e => e.English).HasMaxLength(255).IsUnicode(false);
            });
            builder.OwnsOne(t => t.Body, on =>
            {
                on.Property(e => e.English).HasColumnType("varchar(MAX)");
                //方式2
                //on.Property(e => e.English).HasMaxLength(255).IsUnicode(false);
            });
        }
    }
}
