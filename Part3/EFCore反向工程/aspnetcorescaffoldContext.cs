using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

#nullable disable

namespace EFCore反向工程
{
    public partial class aspnetcorescaffoldContext : DbContext
    {
        //private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(t => t.AddConsole());
        public aspnetcorescaffoldContext()
        {
        }

        public aspnetcorescaffoldContext(DbContextOptions<aspnetcorescaffoldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TPerson> TPersons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=aspnetcorescaffold;Trusted_Connection=True");

                //optionsBuilder.UseLoggerFactory(loggerFactory);
                /*
                optionsBuilder.LogTo(msg =>
                {
                    if (!msg.Contains("CommandExecuting")) return;
                    Console.WriteLine(msg);
                });
                */
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<TPerson>(entity =>
            {
                entity.ToTable("T_Persons");

                //entity.Property(e => e.Id)
                //entity.HasIndex(t => t.Id).IsUnique();

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
