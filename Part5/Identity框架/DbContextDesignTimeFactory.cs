using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity框架
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=aspnetcoreef; Integrated Security=SSPI;Encrypt=false;");
            return new MyDbContext(builder.Options);
        }
    }
}
