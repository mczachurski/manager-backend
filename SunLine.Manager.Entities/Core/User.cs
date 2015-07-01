namespace SunLine.Manager.Entities.Core
{
    public class User : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
