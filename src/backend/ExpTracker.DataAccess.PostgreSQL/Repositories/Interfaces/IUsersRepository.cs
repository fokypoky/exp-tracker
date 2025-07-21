using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces.Common;
using ExpTracker.Entities.Common;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces
{
	public interface IUsersRepository : IEntityRepository<User>
	{
		Task<User?> GetUserByLoginAsync(string login);
	}
}
