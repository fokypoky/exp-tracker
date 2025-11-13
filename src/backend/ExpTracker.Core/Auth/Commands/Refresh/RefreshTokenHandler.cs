using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto.Responses.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.Refresh
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ServiceResponse<JwtTokenPair>>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthUtils _utils;

        public RefreshTokenHandler(IUsersRepository repository, IAuthUtils utils)
        {
            _repository = repository;
            _utils = utils;
        }

        public async Task<ServiceResponse<JwtTokenPair>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (_utils.IsTokenExpired(request.Request.RefreshToken))
                return ServiceResponse<JwtTokenPair>.Unauthorized("Refresh token истек");

            var guid = _utils.GetGuidFromToken(request.Request.RefreshToken);
            var login = _utils.GetLoginFromToken(request.Request.RefreshToken);

            if (guid == null)
                return ServiceResponse<JwtTokenPair>.Unauthorized("Некорректный payload. Отсутствует guid");
            if (login == null)
                return ServiceResponse<JwtTokenPair>.Unauthorized("Некорректный payload. Отсутствует login");

            var user = await _repository.GetAsync(guid.Value);

            if (user == null)
                return ServiceResponse<JwtTokenPair>.Unauthorized($"Пользователь {login} не найден");
            if (!user.RefreshToken.Equals(request.Request.RefreshToken))
                return ServiceResponse<JwtTokenPair>.Unauthorized("Некорректный refresh token");

            var claims = _utils.CreateClaims(user.Id.ToString(), user.Login);
            var tokenPair = _utils.CreateJwtTokenPair(claims);

            user.RefreshToken = tokenPair.RefreshToken;
            await _repository.UpdateAsync(user);

            return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
        }
    }
}
