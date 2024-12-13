using TimeLog.Domain;

namespace TimeLog.Service.Service.Interface
{
    public interface ITimeLogService
    {
        Task<List<Domain.TimeLog>> GetTimeLogsAsync(DateTime? fromDate, DateTime? toDate);

        Task StartStopTimerAsync(int id);

        Task UpdateTimerAsync(int id, string description);

        Task DeleteTimerAsync(int id);

        Task<bool> HasActiveTimer(int id);

        Task<bool> HasTimer(int id);
    }
}
