using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces.Common;
using ExpTracker.Entities.Common;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces
{
    public interface ICategoriesRepository : IEntityRepository<TransactionCategory>
    {
        Task<TransactionCategory?> GetByNameAndUserIdAsync(string name, Guid userId);
        Task<TransactionCategory?> GetByIdAndUserIdAsync(Guid id, Guid userId);
        Task<TransactionCategory?> GetByUserIdAndNameAsync(Guid userId, string name);
        Task<bool> IsRelatedAsync(Guid id);
        Task<PaginatedCollection<TransactionCategory>> GetRangeAsync(Guid userId, int limit, int offset, string search);
        Task<bool> IsExistsAsync(Guid id, Guid userId);
    }
}
