using AutoMapper;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Categories;

namespace ExpTracker.EntitiesMapping.Categories
{
    public class TransactionCategoriesMapper : ITransactionCategoriesMapper
    {
        private readonly IMapper _mapper;

        public TransactionCategoriesMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TransactionCategoryDto Map(TransactionCategory category)
        {
            return _mapper.Map<TransactionCategory, TransactionCategoryDto>(category);
        }

        public TransactionCategory Map(TransactionCategoryDto dto, Guid userId)
        {
            var result = _mapper.Map<TransactionCategoryDto, TransactionCategory>(dto);
            result.UserId = userId;

            return result;
        }

        public TransactionCategory Map(CreateCategoryRequest request, Guid userId)
        {
            var result = _mapper.Map<CreateCategoryRequest, TransactionCategory>(request);
            result.UserId = userId;

            return result;
        }
    }
}
