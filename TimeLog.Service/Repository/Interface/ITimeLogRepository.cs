using TimeLog.Domain;

namespace TimeLog.Service.Repository.Interface
{
    public interface ITimeLogRepository : IGenericRepository<Domain.TimeLog>
    {
        List<Domain.TimeLog> SetLatestStatus(List<Domain.TimeLog> timeLogs);
    }
}
