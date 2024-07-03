using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");
            builder.Property("passwordHash");//添加非属性的成员变量 特征三 
            builder.Property(t => t.Remark).HasField("remark"); //只读属性初始化的列为成员变量而非属性 特征四
            builder.Ignore(t => t.Tag);//忽略字段 特征五
        }
    }
}
