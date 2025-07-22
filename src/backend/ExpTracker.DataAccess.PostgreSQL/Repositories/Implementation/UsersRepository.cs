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

		public async Task<User> CreateAsync(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public Task DeleteAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public async Task<User> UpdateAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			return user;
		}
	}
}
