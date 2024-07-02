using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore自引用的组织结构树
{
     class MyDbContext : DbContext
    {
        public DbSet<OrgUnit> OrgUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //SQLServer 

            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=aspnetcore2;Trusted_Connection=True;MultipleActiveResultSets=true;");
            //optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
