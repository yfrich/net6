using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore表达式树
{
    class BookEConfig : IEntityTypeConfiguration<BookE>
    {
        public void Configure(EntityTypeBuilder<BookE> builder)
        {
            builder.ToTable("T_BookEs");
        }
    }
}
