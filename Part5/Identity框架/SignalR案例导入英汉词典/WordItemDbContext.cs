using Microsoft.EntityFrameworkCore;

namespace Identity框架.SignalR案例导入英汉词典
{
    public class WordItemDbContext : DbContext
    {
        public DbSet<WordItem> wordItems { get; set; }
        public WordItemDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly, t =>
            {
                return typeof(WordItemConfig) == t;
            });
        }
    }
}
