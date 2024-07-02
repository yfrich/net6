using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity框架.SignalR案例导入英汉词典
{
    public class WordItemConfig : IEntityTypeConfiguration<WordItem>
    {
        public void Configure(EntityTypeBuilder<WordItem> builder)
        {
            builder.ToTable("T_WordItems");
        }
    }
}
