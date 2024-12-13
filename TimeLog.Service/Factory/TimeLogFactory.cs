using System.Text.Json;
using TimeLog.Domain;
using TimeLog.Service.Factory.Interface;

namespace TimeLog.Service.Factory
{
    public class TimeLogFactory : ITimeLogFactory
    {
        private readonly HttpClient _httpClient;

        public TimeLogFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public TimeLogFactory()
        {
            
        }

        public async Task<Domain.TimeLog> GetActiveInstanceAsync(int id)
        {
            return new Domain.TimeLog
            {
                TimerId = id,
                Created = DateTime.UtcNow,
                Description = await GetDescripitionAsync(),
                Action = Domain.Action.Start,
            };
        }

        public async Task<Domain.TimeLog> GetInActiveInstanceAsync(int id, string desc)
        {
            return new Domain.TimeLog
            {
                TimerId = id,
                Created = DateTime.UtcNow,
                Action = Domain.Action.Stop,
                Description = desc,
            };
        }

        public async Task<Domain.TimeLog> GetDeleteInstanceAsync(int id, string desc)
        {
            return new Domain.TimeLog
            {
                TimerId = id,
                Created = DateTime.UtcNow,
                Action = Domain.Action.Delete,
                Description = desc,
            };
        }

        public async Task<Domain.TimeLog> GetModifiedInstanceAsync(int id, string desc)
        {
            return new Domain.TimeLog
            {
                TimerId = id,
                Created = DateTime.UtcNow,
                Action = Domain.Action.Modify,
                Description = desc,
            };
        }

        private async Task<string> GetDescripitionAsync()
        {
            var response = await _httpClient.GetStringAsync("https://zenquotes.io/api/random");
            var quote = JsonSerializer.Deserialize<List<Quote>>(response)?.FirstOrDefault();
            return quote.q;
        }
    }
}
