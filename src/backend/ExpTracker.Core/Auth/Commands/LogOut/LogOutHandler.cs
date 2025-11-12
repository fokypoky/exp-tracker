using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.LogOut
{
    public class LogOutHandler : IRequestHandler<LogOutCommand, ServiceResponse<bool?>>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthUtils _utils;

        public LogOutHandler(IUsersRepository repository, IAuthUtils utils)
        {
            _repository = repository;
            _utils = utils;
        }
        
        public async Task<ServiceResponse<bool?>> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            var guid = _utils.GetGuidFromToken(request.Request.RefreshToken);

            if (guid == null)
                return ServiceResponse<bool?>.Unauthorized("Некорректный payload. Отсутствует guid");

            var user = await _repository.GetAsync(guid.Value);

            if (user == null)
                return ServiceResponse<bool?>.Unauthorized("Пользователь не существует");

            if (!user.RefreshToken?.Equals(request.Request.RefreshToken) ?? true)
                return ServiceResponse<bool?>.Unauthorized("Некорректный refresh token");

            if (_utils.IsTokenExpired(user.RefreshToken))
                return ServiceResponse<bool?>.Unauthorized("Refresh token истек");

            user.RefreshToken = null;
            await _repository.UpdateAsync(user);

            return ServiceResponse<bool?>.Created(null);
        }
    }
}
