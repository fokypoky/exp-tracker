using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto.Responses.Profile;
using MediatR;

namespace ExpTracker.Core.Profile.Queries.Get
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, ServiceResponse<ProfileResponse>>
    {
        private readonly IUsersRepository _repository;

        public GetProfileHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<ProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByLoginAsync(request.Login);

            if (user == null) return ServiceResponse<ProfileResponse>.NotFound("Пользователь не найден");

            return ServiceResponse<ProfileResponse>.Ok(new ProfileResponse()
            {
                Guid = user.Id,
                Login = user.Login,
                Registered = user.Registered
            });
        }
    }
}
