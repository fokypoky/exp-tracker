using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Implementation
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ExpTrackerDbContext _context;

        public TransactionsRepository(ExpTrackerDbContext context)
        {
            _context = context;
        }

        public Task<Transaction> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> CreateAsync(Transaction entity)
        {
            await _context.Transactions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
