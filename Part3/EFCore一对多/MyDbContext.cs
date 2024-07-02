using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对多
{
    public class MyDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //SQLServer 

            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=aspnetcore2;Trusted_Connection=True ");
            //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=aspnetcore2;Trusted_Connection=True;MultipleActiveResultSets=true;");
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseBatchEF_MSSQL();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
