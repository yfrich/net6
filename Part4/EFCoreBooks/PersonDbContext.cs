using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        /// <summary>
        /// 支持ASP.NET 中进行注入 而不是在此类中进行注册
        /// </summary>
        /// <param name="options"></param>
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //当前类所在的程序集并且 指定当前类的配置类 否则多个DbContext 的迁移会把所有的DbContext 带上
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly, t =>
            //{
            //    return typeof(PersonConfig) == t;
            //});
        }
    }
}
