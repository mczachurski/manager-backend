using SunLine.Manager.Entities.Football;

namespace SunLine.Manager.DataTransferObjects.Response
{
	public class PlayerDto : BaseEntityDto
	{	
		public PlayerDto(Player player) : base(player)
		{
			FirstName = player.FirstName;
			LastName = player.LastName;
			TeamId = player.TeamId;
			Attack = player.Attack;
			Pass = player.Pass;
			Defense = player.Defense;
			PlayerPosition = player.PlayerPosition.ToString();
			Height = player.Height;
			Weight = player.Weight;
			Age = player.Age;
			FavouriteFoot = player.FavouriteFoot.ToString();
			Price = player.Price;
		}
		
		public string FirstName { get ; set; }
		public string LastName { get ; set; }
        public int TeamId { get; set; }
        public int Attack { get; set; }
        public int Pass { get; set; }
        public int Defense { get; set; }
        public string PlayerPosition { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public string FavouriteFoot { get; set; }
        public double Price { get; set; } 
	}
}