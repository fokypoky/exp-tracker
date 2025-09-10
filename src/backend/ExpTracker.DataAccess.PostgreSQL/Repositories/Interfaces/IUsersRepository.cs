using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces.Common;
using ExpTracker.Entities.Common;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces
{
	public interface IUsersRepository : IEntityRepository<User>
	{
		Task<User?> GetByLoginAsync(string login);
		Task<User?> GetByLoginAndPasswordAsync(string login, string password);
		Task<User?> GetByRefreshTokenAsync(string refreshToken);
	}
}
