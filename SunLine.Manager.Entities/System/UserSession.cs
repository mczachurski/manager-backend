using System;
using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Entities.System
{
    public class UserSession : BaseEntity
    {
        public User User { get; set; }
        public string Host { get; set; }
        public bool IsActive { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public Guid AccessToken { get; set; }
    }
}
