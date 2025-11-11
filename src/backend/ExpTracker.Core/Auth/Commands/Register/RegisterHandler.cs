using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto.Responses.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, ServiceResponse<JwtTokenPair>>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthUtils _utils;

        public RegisterHandler(IUsersRepository repository, IAuthUtils utils)
        {
            _repository = repository;
            _utils = utils;
        }

        public Task<ServiceResponse<JwtTokenPair>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
