using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Infrastructure
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<UserDbContext> builder = new DbContextOptionsBuilder<UserDbContext>();
            builder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=netcoredddSZ; Integrated Security=SSPI;Encrypt=false;");
            return new UserDbContext(builder.Options);
        }
    }
}
