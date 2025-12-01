using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using MediatR;

namespace ExpTracker.Core.Transactions.Commands.Delete
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, ServiceResponse<object>>
    {
        private readonly ITransactionsRepository _repository;

        public DeleteTransactionHandler(ITransactionsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<object>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.GetByIdAndUserIdAsync(request.Id, request.UserId);

            if (transaction == null) return ServiceResponse<object>.Created(new());

            await _repository.DeleteAsync(transaction);

            return ServiceResponse<object>.Created(new());
        }
    }
}
