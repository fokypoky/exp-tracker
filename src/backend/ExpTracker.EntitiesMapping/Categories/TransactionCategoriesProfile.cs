using AutoMapper;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Categories;

namespace ExpTracker.EntitiesMapping.Categories
{
    public class TransactionCategoriesProfile : Profile
    {
        public TransactionCategoriesProfile()
        {
            CreateMap<TransactionCategory, TransactionCategoryDto>()
                .ReverseMap();
            CreateMap<CreateCategoryRequest, TransactionCategory>();
        }
    }
}
