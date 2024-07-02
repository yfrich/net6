using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore自引用的组织结构树
{
    class OrgUnitConfig : IEntityTypeConfiguration<OrgUnit>
    {
        public void Configure(EntityTypeBuilder<OrgUnit> builder)
        {
            builder.ToTable("T_OrgUnits");
            builder.Property(t => t.Name).HasMaxLength(50).IsUnicode().IsRequired();
            builder.HasOne(t => t.Parent).WithMany(t => t.Children);//根节点没有Parent 所以不能修饰为"不可为空"
        }
    }
}
