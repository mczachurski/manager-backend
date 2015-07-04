using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.Entities.Core
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Email})";
        }
    }
}
