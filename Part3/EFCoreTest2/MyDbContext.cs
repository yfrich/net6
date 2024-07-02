using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTest2
{
    class MyDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //SQLServer 

            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=aspnetcore1;Trusted_Connection=True");

            //MySQL
            /*
            optionsBuilder.UseMySql("server=localhost;user=root;password=Yf112233445!;database=EFCore",
                new MySqlServerVersion(new Version(8, 0, 36)));
            */
            //PostGreSQL
            /*
            var connectionString = "Host=127.0.0.1;Database=EFCore;Username=postgres;Password=Yf112233445!";
            optionsBuilder.UseNpgsql(connectionString);
            */

            optionsBuilder.LogTo(Console.WriteLine);
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
