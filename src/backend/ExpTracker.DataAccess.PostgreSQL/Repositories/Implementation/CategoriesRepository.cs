using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Implementation
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ExpTrackerDbContext _context;

        public CategoriesRepository(ExpTrackerDbContext context)
        {
            _context = context;
        }   

        public Task<TransactionCategory?> GetByNameAndUserIdAsync(string name, Guid userId)
        {
            return _context.TransactionCategories.FirstOrDefaultAsync(_ => _.Name == name && _.UserId == userId);
        }

        public async Task<TransactionCategory> CreateAsync(TransactionCategory entity)
        {
            var result = await _context.TransactionCategories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<PaginatedCollection<TransactionCategory>> GetRangeAsync(Guid userId, int limit, int offset)
        {
            var query = _context.TransactionCategories.Where(_ => _.UserId == userId);

            return new PaginatedCollection<TransactionCategory>()
            {
                Data = await query.Skip(offset).Take(limit).ToListAsync(),
                TotalCount = await query.CountAsync(),
            };
        }

        public Task DeleteAsync(TransactionCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionCategory> GetAsync(Guid id)
        {
            return _context.TransactionCategories.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task<TransactionCategory?> GetByIdAndUserIdAsync(Guid id, Guid userId)
        {
            return _context.TransactionCategories.FirstOrDefaultAsync(_ => _.Id == id && _.UserId == userId);
        }
        
        public Task<TransactionCategory> UpdateAsync(TransactionCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
