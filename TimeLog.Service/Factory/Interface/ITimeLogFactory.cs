
namespace TimeLog.Service.Factory.Interface
{
    public interface ITimeLogFactory
    {
        Task<Domain.TimeLog> GetActiveInstanceAsync(int id);

        Task<Domain.TimeLog> GetInActiveInstanceAsync(int id, string desc);

        Task<Domain.TimeLog> GetDeleteInstanceAsync(int id, string desc);

        Task<Domain.TimeLog> GetModifiedInstanceAsync(int id, string desc);
    }
}
