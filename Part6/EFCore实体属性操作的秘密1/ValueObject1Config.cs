using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class ValueObject1Config : IEntityTypeConfiguration<ValueObject1>
    {
        public void Configure(EntityTypeBuilder<ValueObject1> builder)
        {
            builder.Property(t => t.Currency).HasConversion<string>();
        }
    }
}
