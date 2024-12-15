using TimeLog.Service.Repository.Interface;
using TimeLog.Infrastructure.Context;
using System.Security.Cryptography.X509Certificates;

namespace TimeLog.Service.Repository
{
    public class TimeLogRepository : GenericRepository<Domain.TimeLog>, ITimeLogRepository
    {
        public TimeLogRepository(TimeLogContext context) : base(context)
        {
            
        }

        public List<Domain.TimeLog> SetLatestStatus(List<Domain.TimeLog> timeLogs)
        {
            List<Domain.TimeLog> resultList = new List<Domain.TimeLog>();

            foreach (var item in timeLogs)
            {
                var lastTimerLog = timeLogs.Where(x => x.TimerId == item.TimerId).LastOrDefault();

                if (!resultList.Any(x => x.TimerId == item.TimerId))
                {
                    if (lastTimerLog != null && lastTimerLog.Action == Domain.Action.Modify)
                    {
                        lastTimerLog.Action = Domain.Action.Start;
                    }

                    resultList.Add(lastTimerLog);
                }
            }

            return resultList;
        }
    }
}
