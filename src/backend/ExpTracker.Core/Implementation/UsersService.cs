using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;

namespace ExpTracker.Core.Implementation
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository _repository;

		public UsersService(IUsersRepository repository)
		{
			_repository = repository;
		}

		public async Task<ServiceResponse<User>> GetUserByLoginAsync(string login)
		{
			var user = await _repository.GetUserByLoginAsync(login);

			return new ServiceResponse<User>()
			{
				Result = user != null ? ResponseResult.Ok : ResponseResult.NotFound,
				Data = user,
				Error = user != null ? null : $"User {login} not exists"
			};
		}
	}
}
