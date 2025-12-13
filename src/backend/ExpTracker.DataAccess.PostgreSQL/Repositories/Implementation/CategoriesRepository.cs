using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Models.Filters;
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

        public async Task<PaginatedCollection<TransactionCategory>> GetRangeAsync(Guid userId, CategoryFilters filters)
        {
            var query = _context.TransactionCategories
                .Where(e => e.UserId == userId);

            if (!String.IsNullOrEmpty(filters.Search))
            {
                var lowerSearch = filters.Search.ToLower();
                query = query.Where(e => e.Name.ToLower().Contains(lowerSearch) ||
                                         (e.Description != null && e.Description.ToLower().Contains(lowerSearch)));
            }

            return new PaginatedCollection<TransactionCategory>()
            {
                Data = await query.Skip(filters.Offset).Take(filters.Limit).ToListAsync(),
                TotalCount = await query.CountAsync(),
            };
        }

        public Task<List<TransactionCategory>> GetRangeAsync(Guid userId)
        {
            return _context.TransactionCategories.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id, Guid userId)
        {
            var count = await _context.TransactionCategories.CountAsync(_ => _.Id == id && _.UserId == userId);

            return count > 0;
        }

        public async Task DeleteAsync(TransactionCategory entity)
        {
            _context.TransactionCategories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<TransactionCategory> GetAsync(Guid id)
        {
            return _context.TransactionCategories.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task<TransactionCategory?> GetByIdAndUserIdAsync(Guid id, Guid userId)
        {
            return _context.TransactionCategories.FirstOrDefaultAsync(_ => _.Id == id && _.UserId == userId);
        }

        public Task<TransactionCategory?> GetByUserIdAndNameAsync(Guid userId, string name)
        {
            return _context.TransactionCategories.FirstOrDefaultAsync(_ => _.UserId == userId && _.Name == name);
        }

        public Task<bool> IsRelatedAsync(Guid id)
        {
            return _context.Transactions.Where(t => t.CategoryId == id).AnyAsync();
        }

        public async Task<TransactionCategory> UpdateAsync(TransactionCategory entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
