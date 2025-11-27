using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using MediatR;

namespace ExpTracker.Core.Transactions.Commands.Create
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, ServiceResponse<TransactionDto>>
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public CreateTransactionHandler(ITransactionsRepository transactionsRepository, ICategoriesRepository categoriesRepository)
        {
            _transactionsRepository = transactionsRepository;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ServiceResponse<TransactionDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepository.GetAsync(request.Request.CategoryId);

            if (category == null) return ServiceResponse<TransactionDto>.NotFound("Категория не найдена");

            var transaction = new Transaction()
            {
                Category = category,
                Cost = request.Request.Cost,
                Date = request.Request.Date, Description = request.Request.Description,
                IntervalType = request.Request.IntervalType, Type = request.Request.Type,
                UserId = request.UserId,
            };

            var result = await _transactionsRepository.CreateAsync(transaction);

            throw new NotImplementedException();
        }
    }
}
