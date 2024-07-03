using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ValueObject1> ValueObject1s { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        //订单聚合根
        public DbSet<Order> Orders { get; set; }
        //商品聚合根
        public DbSet<Merchan> Merchans { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=netcoreddd; Integrated Security=SSPI;Encrypt=false;");
            optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly, t =>
            //{
            //    return t == typeof(UserConfig);
            //});
        }
    }
}
