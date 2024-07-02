using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对多
{
    class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("T_Articles");
            builder.Property(t => t.Title).HasMaxLength(100).IsUnicode().IsRequired();
            builder.Property(t => t.Message).IsUnicode().IsRequired();
            builder.HasMany<Comment>(t => t.Comments)//设置一的多项
                .WithOne(t => t.TheArticle)//设置多的对象
                .HasForeignKey(t => t.TheArticleId)//设置外键
                .IsRequired();
            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}
