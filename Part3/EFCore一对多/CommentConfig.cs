using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对多
{
    class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.Property(t => t.Message).IsUnicode().IsRequired();
            //builder.HasOne<Article>(t => t.TheArticle)//设置一的多项
            //    .WithMany(t => t.Comments)//设置多的对象
            //    .HasForeignKey(t => t.TheArticleId)//设置外键
            //    .IsRequired();
        }
    }
}
