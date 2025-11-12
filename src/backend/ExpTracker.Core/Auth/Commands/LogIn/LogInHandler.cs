using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto.Responses.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.LogIn
{
    public class LogInHandler : IRequestHandler<LogInCommand, ServiceResponse<JwtTokenPair>>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthUtils _utils;

        public LogInHandler(IUsersRepository repository, IAuthUtils utils)
        {
            _repository = repository;
            _utils = utils;
        }

        public async Task<ServiceResponse<JwtTokenPair>> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var hash = _utils.HashPassword(request.Request.Password);
            var user = await _repository.GetByLoginAndPasswordAsync(request.Request.Login, hash);

            if (user == null)
                return ServiceResponse<JwtTokenPair>.NotFound($"Пользователь с такими данными не найден");

            var claims = _utils.CreateClaims(user.Id.ToString(), user.Login);
            var tokenPair = _utils.CreateJwtTokenPair(claims);

            user.RefreshToken = tokenPair.RefreshToken;

            await _repository.UpdateAsync(user);

            return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
        }
    }
}
