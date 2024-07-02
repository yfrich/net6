using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity框架.SignalR案例导入英汉词典
{
    public class WordItemDesignTimeFactory : IDesignTimeDbContextFactory<WordItemDbContext>
    {
        public WordItemDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<WordItemDbContext> builder = new DbContextOptionsBuilder<WordItemDbContext>();
            builder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=aspnetcoreef; Integrated Security=SSPI;Encrypt=false;");
            return new WordItemDbContext(builder.Options);
        }
    }
}
