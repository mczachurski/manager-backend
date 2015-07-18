namespace SunLine.Manager.Entities.Football
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public int Attack { get; set; }
        public int Pass { get; set; }
        public int Defense { get; set; }
        public PlayerPositionEnum PlayerPosition { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public FavouriteFootEnum FavouriteFoot { get; set; }
        public double Price { get; set; } 
        
        public int PlayerRate 
        {
            get 
            {
                return Attack + Pass + Defense;
            }
        }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Team})";
        }
    }
}
