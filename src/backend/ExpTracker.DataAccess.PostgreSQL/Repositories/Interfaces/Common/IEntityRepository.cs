namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces.Common
{
	public interface IEntityRepository<T> where T : class
	{
		Task<T> GetAsync(Guid id);
		Task<T> CreateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<T> UpdateAsync(T entity);
	}
}
