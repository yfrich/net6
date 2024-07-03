using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class ShopConfig : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shop");
            //如何配置值对象  从属实体类型
            builder.OwnsOne(t => t.Location, a =>
            {
                //如何设置从属类型的列的信息
                a.Property(e => e.Longitude).HasMaxLength(3);
            });// ownde entity
        }
    }
}
