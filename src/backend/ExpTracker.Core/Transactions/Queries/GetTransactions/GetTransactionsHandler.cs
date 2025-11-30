using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto;
using ExpTracker.EntitiesMapping.Transactions;
using MediatR;

namespace ExpTracker.Core.Transactions.Queries.GetTransactions
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, ServiceResponse<PaginatedResponse<TransactionDto>>>
    {
        private readonly ITransactionsRepository _repository;
        private readonly ITransactionsMapper _mapper;

        public GetTransactionsHandler(ITransactionsRepository repository, ITransactionsMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PaginatedResponse<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var filters = _mapper.MapFilters(request.Request);

            var result = await _repository.GetRangeAsync(request.UserId, filters);

            return ServiceResponse<TransactionDto>.Partial(
                result.Data.Select(x => _mapper.Map(x)).ToList(),
                result.TotalCount
            );
        }
    }
}
