using ExpTracker.DataAccess.PostgreSQL.Models.Filters;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Categories;

namespace ExpTracker.EntitiesMapping.Categories
{
    public interface ITransactionCategoriesMapper
    {
        TransactionCategoryDto Map(TransactionCategory transaction);
        TransactionCategory Map(TransactionCategoryDto dto, Guid userId);
        TransactionCategory Map(CreateCategoryRequest request, Guid userId);

        CategoryFilters MapFilters(GetCategoriesRequest request);
    }
}
