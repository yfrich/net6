using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBooks
{
    /// <summary>
    /// 此接口实现就是为了使用Migration 用于生成数据库脚本。
    /// 开发时使用(Add_Migration、Update-Database) 程序运行的时候不会有这个类的事儿
    /// </summary>
    internal class BookDBContextDesignFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<BookDbContext> builder = new DbContextOptionsBuilder<BookDbContext>();
            string? connStr = Environment.GetEnvironmentVariable("ConnStrEF");
            builder.UseSqlServer(connStr);
            BookDbContext context = new BookDbContext(builder.Options);
            return context;
        }
    }
}
