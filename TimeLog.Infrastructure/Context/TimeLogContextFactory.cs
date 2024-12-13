using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace TimeLog.Infrastructure.Context
{
    public class TimeLogContextFactory : IDesignTimeDbContextFactory<TimeLogContext>
    {
        public TimeLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TimeLogContext>();
            optionsBuilder.UseSqlite("Data Source=TimeLog.db");

            return new TimeLogContext(optionsBuilder.Options);
        }
    }
}
