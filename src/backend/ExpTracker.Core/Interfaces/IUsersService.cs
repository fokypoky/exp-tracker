using ExpTracker.Core.Models;
using ExpTracker.Entities.Common;

namespace ExpTracker.Core.Interfaces
{
	public interface IUsersService
	{
		Task<ServiceResponse<User>> GetUserByLoginAsync(string login);
		Task<ServiceResponse<User>> CreateAsync(Guid guid, string login, string password, string refreshToken);
		Task<ServiceResponse<User>> UpdateRefreshTokenAsync(User user, string refreshToken);
	}
}
