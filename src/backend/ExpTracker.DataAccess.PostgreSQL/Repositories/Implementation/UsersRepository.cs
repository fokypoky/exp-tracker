using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Implementation
{
	public class UsersRepository : IUsersRepository
	{
		private readonly ExpTrackerDbContext _context;

		public UsersRepository(ExpTrackerDbContext context)
		{
			_context = context;
		}

		public Task<User> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<User?> GetUserByLoginAsync(string login)
		{
			return _context.Users.FirstOrDefaultAsync(_ => _.Login == login);
		}

		public Task<User> CreateAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public Task<User> UpdateAsync(User entity)
		{
			throw new NotImplementedException();
		}
	}
}
