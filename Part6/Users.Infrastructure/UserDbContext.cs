using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Domain.Entities;

namespace Users.Infrastructure
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
