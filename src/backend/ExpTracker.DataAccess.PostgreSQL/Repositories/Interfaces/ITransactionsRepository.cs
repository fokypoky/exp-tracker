using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Models.Filters;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces.Common;
using ExpTracker.Entities.Common;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces
{
    public interface ITransactionsRepository : IEntityRepository<Transaction>
    {
        Task<PaginatedCollection<Transaction>> GetRangeAsync(Guid userId, TransactionFilters filters);
        Task<Transaction?> GetByIdWithCategoryAsync(Guid id, Guid userId);
        Task<Transaction?> GetByIdAndUserIdAsync(Guid id, Guid userId);
    }
}
