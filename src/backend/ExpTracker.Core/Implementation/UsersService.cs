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

		public async Task<ServiceResponse<User>> GetByIdAsync(Guid guid)
		{
			var user = await _repository.GetAsync(guid);

			return user == null
				? ServiceResponse<User>.NotFound("User")
				: ServiceResponse<User>.Ok(user);
		}

		public async Task<ServiceResponse<User>> GetByLoginAsync(string login)
		{
			var user = await _repository.GetByLoginAsync(login);

			return user == null
				? ServiceResponse<User>.NotFound($"User {login}")
				: ServiceResponse<User>.Ok(user);
		}

		public async Task<ServiceResponse<User>> GetByLoginAndPasswordAsync(string login, string password)
		{
			var user = await _repository.GetByLoginAndPasswordAsync(login, password);

			return user == null
				? ServiceResponse<User>.NotFound("User with credentials")
				: ServiceResponse<User>.Ok(user);
		}

		public async Task<ServiceResponse<User>> GetByRefreshTokenAsync(string refreshToken)
		{
			var user = await _repository.GetByRefreshTokenAsync(refreshToken);

			return user == null
				? ServiceResponse<User>.NotFound("User")
				: ServiceResponse<User>.Ok(user);
		}

		public async Task<ServiceResponse<User>> CreateAsync(Guid guid, string login, string password, string refreshToken)
		{
			var user = new User() { Id = guid, Login = login, Password = password, RefreshToken = refreshToken };

			await _repository.CreateAsync(user);

			return ServiceResponse<User>.Ok(user);
		}

		public async Task<ServiceResponse<User>> UpdateRefreshTokenAsync(User user, string? refreshToken)
		{
			user.RefreshToken = refreshToken;
			await _repository.UpdateAsync(user);

			return ServiceResponse<User>.Ok(user);
		}
	}
}
