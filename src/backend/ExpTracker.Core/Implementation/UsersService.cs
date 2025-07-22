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

		public async Task<ServiceResponse<User>> CreateAsync(Guid guid, string login, string password, string refreshToken)
		{
			var user = new User() { Id = guid, Login = login, Password = password, RefreshToken = refreshToken };

			var result = await _repository.CreateAsync(user);

			return ServiceResponse<User>.Ok(user);
		}

		public async Task<ServiceResponse<User>> UpdateRefreshTokenAsync(User user, string refreshToken)
		{
			user.RefreshToken = refreshToken;
			await _repository.UpdateAsync(user);

			return ServiceResponse<User>.Ok(user);
		}
	}
}
