using Action = TimeLog.Domain.Action;

namespace TimeLog.Service.Response
{
    public class TimeLogDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Action Action { get; set; }
    }
}
