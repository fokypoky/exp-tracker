using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Models.Filters;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaginatedCollection<Transaction>> GetRangeAsync(Guid userId, TransactionFilters filters)
        {
            var query = _context.Transactions
                .Include(x => x.Category)
                .Where(x => x.UserId == userId);

            if (!String.IsNullOrEmpty(filters.Search))
            {
                var lowerSearch = filters.Search.ToLower();
                query = query.Where(x => x.Description.ToLower().Contains(lowerSearch));
            }

            if (filters.DateFrom != null)
            {
                query = query.Where(x => x.Date >= filters.DateFrom);
            }

            if (filters.DateTo != null)
            {
                query = query.Where(x => x.Date <= filters.DateTo);
            }

            return new PaginatedCollection<Transaction>()
            {
                Data = await query.Skip(filters.Offset).Take(filters.Limit).ToListAsync(),
                TotalCount = await query.CountAsync()
            };
        }

        public Task<Transaction?> GetByIdWithCategoryAsync(Guid id, Guid userId)
        {
            return _context.Transactions
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public Task<Transaction?> GetByIdAndUserIdAsync(Guid id, Guid userId)
        {
            return _context.Transactions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<Transaction> CreateAsync(Transaction entity)
        {
            await _context.Transactions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Transaction entity)
        {
            _context.Transactions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<Transaction> UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
