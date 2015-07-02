namespace SunLine.Manager.Entities.Core
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Team Team { get; set; }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Team})";
        }
    }
}
