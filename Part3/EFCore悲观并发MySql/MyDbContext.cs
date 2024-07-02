using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore悲观并发MySql
{
    class MyDbContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //SQLServer 

            optionsBuilder.UseMySql("server=localhost;user=root;password=Yf112233445!;database=EFCore",
                    new MySqlServerVersion(new Version(8, 0, 36)));
            //optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
