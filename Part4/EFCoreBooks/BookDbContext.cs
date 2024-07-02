using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        /// <summary>
        /// 支持ASP.NET 中进行注入 而不是在此类中进行注册
        /// </summary>
        /// <param name="options"></param>
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //当前类所在的程序集
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly, t =>
            {
                return typeof(BookConfig) == t;
            });
        }
    }
}
