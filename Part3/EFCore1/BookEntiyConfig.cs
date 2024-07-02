using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore1
{
    /// <summary>
    /// 配置类
    /// </summary>
    class BookEntiyConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //T_  s
            //设置实体对应哪个表
            builder.ToTable("T_Books");
            builder.Property(t => t.Title).HasMaxLength(50).IsRequired();
            builder.Property(t => t.AuthorName).HasMaxLength(20).IsRequired();

            builder.Ignore(t => t.Age2);
            builder.Property(t => t.Name2).HasColumnName("nametwo").HasColumnType("varchar(8)").HasMaxLength(20);

            //唯一索引
            builder.HasIndex(t => t.Title).IsUnique();
            //复合索引
            builder.HasIndex(t => new { t.Name2, t.AuthorName });

            //举例 测试写法
            /*
            builder.ToTable("T_Books");
            builder.HasIndex(t => t.Title);
            builder.Ignore(t => t.PubTime);
            */
            //
            /*
            builder.ToTable("T_Books").HasIndex(t => t.Title);
            builder.Ignore(t => t.PubTime);
            builder.ToTable("T_Books").HasIndex(t => t.Title).Ignore(t>t.PubTime)
            */
        }
    }
}
