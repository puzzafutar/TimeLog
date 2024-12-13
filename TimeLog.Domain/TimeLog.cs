using System.ComponentModel.DataAnnotations;

namespace TimeLog.Domain
{
    public class TimeLog
    {
        [Key]
        public int Id { get; set; }

        public int TimerId { get; set; }

        public Action Action { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}
