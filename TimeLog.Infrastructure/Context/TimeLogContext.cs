using Microsoft.EntityFrameworkCore;

namespace TimeLog.Infrastructure.Context
{
    public class TimeLogContext : DbContext
    {
        public DbSet<Domain.TimeLog> TimeLog { get; set; }

        public TimeLogContext(DbContextOptions<TimeLogContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TimeLog.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.TimeLog>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
