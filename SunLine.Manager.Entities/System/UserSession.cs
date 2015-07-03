using System;
using SunLine.Manager.Entities.Core;

namespace SunLine.Manager.Entities.System
{
    public class UserSession : BaseEntity
    {
        public virtual User User { get; set; }
        public virtual string Host { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime SessionStart { get; set; }
        public virtual DateTime? SessionEnd { get; set; }
        public virtual Guid AccessToken { get; set; }
    }
}
