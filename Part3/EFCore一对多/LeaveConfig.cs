using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对多
{
    class LeaveConfig : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.ToTable("T_Leaves");
            builder.HasOne(t => t.Requester).WithMany().IsRequired();
            builder.HasOne(t => t.Approver).WithMany();
        }
    }
}
