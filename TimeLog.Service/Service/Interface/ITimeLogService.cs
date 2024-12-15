using TimeLog.Domain;

namespace TimeLog.Service.Service.Interface
{
    public interface ITimeLogService
    {
        Task<List<Domain.TimeLog>> GetTimeLogsAsync(DateTime? fromDate, DateTime? toDate, int currentPage, int pageSize);

        Task StartStopTimerAsync(int id);

        Task UpdateTimerAsync(int id, string description);

        Task DeleteTimerAsync(int id);

        Task<bool> HasActiveTimerAsync(int id);

        Task<bool> HasTimerAsync(int id);
    }
}
