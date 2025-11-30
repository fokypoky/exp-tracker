using AutoMapper;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Transactions;

namespace ExpTracker.EntitiesMapping.Transactions
{
    public class TransactionsProfile : Profile
    {
        public TransactionsProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();
            CreateMap<CreateTransactionRequest, Transaction>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
