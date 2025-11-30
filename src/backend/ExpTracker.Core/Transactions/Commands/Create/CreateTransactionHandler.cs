using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Transactions;
using MediatR;

namespace ExpTracker.Core.Transactions.Commands.Create
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, ServiceResponse<TransactionDto>>
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ITransactionsMapper _mapper;

        public CreateTransactionHandler(ITransactionsRepository transactionsRepository, ICategoriesRepository categoriesRepository, ITransactionsMapper mapper)
        {
            _transactionsRepository = transactionsRepository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TransactionDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepository.GetAsync(request.Request.CategoryId);

            if (category == null) return ServiceResponse<TransactionDto>.NotFound("Категория не найдена");

            var mappedTransaction = _mapper.Map(request.Request, request.UserId);

            var result = await _transactionsRepository.CreateAsync(mappedTransaction);

            var mappedResult = _mapper.Map(result);
            
            return ServiceResponse<TransactionDto>.Ok(mappedResult);
        }
    }
}
