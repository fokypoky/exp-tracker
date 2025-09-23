using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces.Common;
using ExpTracker.Entities.Common;

namespace ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces
{
    public interface ICategoriesRepository : IEntityRepository<TransactionCategory>
    {
        Task<TransactionCategory?> GetByNameAndUserIdAsync(string name, Guid userId);
        Task<PaginatedCollection<TransactionCategory>> GetRangeAsync(Guid userId, int limit, int offset);
    }
}
