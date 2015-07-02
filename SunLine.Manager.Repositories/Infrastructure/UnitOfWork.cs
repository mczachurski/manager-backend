namespace SunLine.Manager.Repositories.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private DatabaseContext _databaseContext;
		
		public UnitOfWork(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}		
		
		public void Commit()
		{
			_databaseContext.SaveChanges();
		}
	}
}