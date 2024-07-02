using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore多对多
{
    class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("T_Students");
            builder.HasMany(t => t.Teachers).WithMany(t => t.Students).UsingEntity(t => t.ToTable("T_Students_Teachers"));
        }
    }
}
