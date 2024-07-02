using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks
{
    internal class PersonDBContextDesignFactory : IDesignTimeDbContextFactory<PersonDbContext>
    {
        public PersonDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<PersonDbContext> builder = new DbContextOptionsBuilder<PersonDbContext>();
            string connStr = Environment.GetEnvironmentVariable("ConnStrEF");
            builder.UseSqlServer(connStr);
            PersonDbContext context = new PersonDbContext(builder.Options);
            return context;
        }
    }
}
