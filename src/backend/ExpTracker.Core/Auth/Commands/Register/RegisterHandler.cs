using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
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

        public async Task<ServiceResponse<JwtTokenPair>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetByLoginAsync(request.Request.Login);

            if (existingUser != null)
                return ServiceResponse<JwtTokenPair>.BadRequest($"Пользователь {request.Request.Login} уже существует");

            var hash = _utils.HashPassword(request.Request.Password);
            var guid = Guid.NewGuid();

            var claims = _utils.CreateClaims(guid.ToString(), request.Request.Login);
            var tokenPair = _utils.CreateJwtTokenPair(claims);

            var user = new User()
            {
                Id = guid,
                Login = request.Request.Login,
                Password = hash,
                RefreshToken = tokenPair.RefreshToken,
                Registered = DateTime.UtcNow
            };

            await _repository.CreateAsync(user);

            return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
        }
    }
}
