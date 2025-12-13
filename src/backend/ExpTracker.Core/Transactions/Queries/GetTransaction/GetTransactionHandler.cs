using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Transactions;
using MediatR;

namespace ExpTracker.Core.Transactions.Queries.GetTransaction
{
    public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, ServiceResponse<TransactionDto>>
    {
        private readonly ITransactionsRepository _repository;
        private readonly ITransactionsMapper _mapper;

        public GetTransactionHandler(ITransactionsRepository repository, ITransactionsMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.GetByIdWithCategoryAsync(request.Id, request.UserId);

            if (transaction == null) return ServiceResponse<TransactionDto>.NotFound("Транзакция не найдена");

            var mappedTransaction = _mapper.Map(transaction);

            return ServiceResponse<TransactionDto>.Ok(mappedTransaction);
        }
    }
}
