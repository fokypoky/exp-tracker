using ExpTracker.Core.Models;
using ExpTracker.Entities.Common;

namespace ExpTracker.Core.Interfaces
{
	public interface IUsersService
	{
		Task<ServiceResponse<User>> GetUserByLoginAsync(string login);
	}
}
