
using Microsoft.EntityFrameworkCore;
using TimeLog.Service.Factory.Interface;
using TimeLog.Service.Repository.Interface;
using TimeLog.Service.Service.Interface;

namespace TimeLog.Service.Service
{
    public class TimeLogService : ITimeLogService
    {
        private readonly ITimeLogRepository _timeLogRepository;
        private readonly ITimeLogFactory _timeLogFactory;

        public TimeLogService(
            ITimeLogRepository timeLogRepository,
            ITimeLogFactory timeLogFactory)
        {
            _timeLogRepository = timeLogRepository;
            _timeLogFactory = timeLogFactory;
        }

        public async Task DeleteTimerAsync(int id)
        {
            var entityList = await _timeLogRepository.GetAllAsync();
            if (!entityList.Where(x => x.TimerId == id && x.Action == Domain.Action.Delete).Any())
            {
                string desc = entityList.Where(x => x.TimerId == id).LastOrDefault()?.Description;
                var deleteLog = await _timeLogFactory.GetDeleteInstanceAsync(id, desc);
                await _timeLogRepository.AddAsync(deleteLog);
            }
        }

        public async Task<List<Domain.TimeLog>> GetTimeLogsAsync(DateTime? fromDate, DateTime? toDate, int currentPage, int pageSize)
        {
            var query = _timeLogRepository.AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.Created <= fromDate);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.Created >= toDate);
            }

            var partResultList = await query.ToListAsync();

            List<Domain.TimeLog> resultList = new List<Domain.TimeLog>();
            foreach (var item in partResultList)
            {
                var lastTimerLog = partResultList.Where(x => x.TimerId == item.TimerId).LastOrDefault();

                if (!resultList.Any(x => x.TimerId == item.TimerId))
                {
                    if (lastTimerLog != null && lastTimerLog.Action == Domain.Action.Modify)
                    {
                        lastTimerLog.Action = Domain.Action.Start;
                    }

                    resultList.Add(lastTimerLog);
                }
            }

            if (currentPage > 0 && pageSize > 0)
            {
                var totalCount = resultList.Count;
                int TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                int CurrentPage = currentPage;
                List<Domain.TimeLog> Items = resultList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return Items;
            }
            else
            {
                return resultList;
            }
            
        }

        public async Task<bool> HasActiveTimer(int id)
        {
            var query = _timeLogRepository.AsQueryable();

            query = query.Where(x => x.TimerId == id);

            var timerLogs = await query.ToListAsync();

            var lastLog = timerLogs.LastOrDefault();

            return lastLog != null && (lastLog.Action != Domain.Action.Delete && lastLog.Action != Domain.Action.Stop);
        }

        public async Task<bool> HasTimer(int id)
        {
            var query = _timeLogRepository.AsQueryable();

            query = query.Where(x => x.TimerId == id);

            var timerLogs = await query.ToListAsync();

            var lastLog = timerLogs.LastOrDefault();

            return lastLog != null && (lastLog.Action != Domain.Action.Delete);
        }

        public async Task StartStopTimerAsync(int stopIndex)
        {
            if (stopIndex == 0)
            {
                var result = await _timeLogRepository.GetAllAsync();
                int counter = result.GroupBy(x => x.TimerId).Count() + 1;

                var timelogEntity = await _timeLogFactory.GetActiveInstanceAsync(counter);
                await _timeLogRepository.AddAsync(timelogEntity);

                return;
            }
            else
            {
                var entityList = await _timeLogRepository.GetAllAsync();
                var lastLogEntity = entityList.Where(x => x.TimerId == stopIndex).LastOrDefault();

                if (lastLogEntity != null && (lastLogEntity.Action != Domain.Action.Stop || lastLogEntity.Action != Domain.Action.Delete))
                {
                    var entity = await _timeLogFactory.GetInActiveInstanceAsync(stopIndex, lastLogEntity.Description);
                    await _timeLogRepository.AddAsync(entity);
                }

                return;
            }
        }

        public async Task UpdateTimerAsync(int id, string description)
        {
            var entity = await _timeLogFactory.GetModifiedInstanceAsync(id, description);
            await _timeLogRepository.AddAsync(entity);
            return;
        }
    }
}
