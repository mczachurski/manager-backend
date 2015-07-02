namespace SunLine.Manager.Repositories.Infrastructure
{
	public interface IUnitOfWork
	{
		void Commit();
	}
}