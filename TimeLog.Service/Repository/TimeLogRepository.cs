using TimeLog.Service.Repository.Interface;
using TimeLog.Infrastructure.Context;

namespace TimeLog.Service.Repository
{
    public class TimeLogRepository : GenericRepository<Domain.TimeLog>, ITimeLogRepository
    {
        public TimeLogRepository(TimeLogContext context) : base(context)
        {
        }
    }
}
