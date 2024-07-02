using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore1
{
    /// <summary>
    /// DbContext
    /// </summary>
    class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        /// <summary>
        /// 未建立约定配置，则默认取当前对象名字为表明
        /// </summary>
        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Bird> BirdsHahaha { get; set; }

        public DbSet<Rabbit> Rabbits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //指定连接字符串
            string connStr = @"Server=localhost\SQLEXPRESS01;Database=aspnetcore;Trusted_Connection=True";
            //指定连接的数据库
            optionsBuilder.UseSqlServer(connStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //指定我的配置类所在的程序集
            //从当前程序加加载所有的 所有实现了IEntityTypeConfiguration 的接口类
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
