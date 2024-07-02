using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore1
{
    class BirdEntityConfig : IEntityTypeConfiguration<Bird>
    {
        public void Configure(EntityTypeBuilder<Bird> builder)
        {
            builder.HasKey(t => t.Number);
            builder.Property(t => t.Name).HasDefaultValue("hello");
            //不推荐
            //builder.Property("Name").HasDefaultValue("XXX");
        }
    }
}
