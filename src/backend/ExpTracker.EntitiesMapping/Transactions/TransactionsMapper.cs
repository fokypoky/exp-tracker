using AutoMapper;
using ExpTracker.DataAccess.PostgreSQL.Models.Filters;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Transactions;

namespace ExpTracker.EntitiesMapping.Transactions
{
    public class TransactionsMapper : ITransactionsMapper
    {
        private readonly IMapper _mapper;

        public TransactionsMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Transaction Map(TransactionDto dto, Guid userId)
        {
            var result = _mapper.Map<TransactionDto, Transaction>(dto);
            result.UserId = userId;

            return result;
        }

        public Transaction Map(CreateTransactionRequest request, Guid userId)
        {
            var result = _mapper.Map<CreateTransactionRequest, Transaction>(request);
            result.UserId = userId;

            return result;
        }

        public TransactionDto Map(Transaction transaction)
        {
            var result = _mapper.Map<Transaction, TransactionDto>(transaction);
            return result;
        }

        public TransactionFilters MapFilters(GetTransactionsRequest request)
        {
            var result = _mapper.Map<GetTransactionsRequest, TransactionFilters>(request);
            return result;
        }
    }
}
