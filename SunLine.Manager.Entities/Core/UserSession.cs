using System;

namespace SunLine.Manager.Entities.Core
{
    public class UserSession : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string Host { get; set; }
        public bool IsActive { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public Guid AccessToken { get; set; }
    }
}
