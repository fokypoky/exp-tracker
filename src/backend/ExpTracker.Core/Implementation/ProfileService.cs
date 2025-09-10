using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Dto.Responses.Profile;

namespace ExpTracker.Core.Implementation
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersRepository _repository;

        public ProfileService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<ProfileResponse>> GetAsync(string login)
        {
            var user = await _repository.GetByLoginAsync(login);

            if (user == null) return ServiceResponse<ProfileResponse>.NotFound(login);

            return ServiceResponse<ProfileResponse>.Ok(new ProfileResponse()
            {
                Guid = user.Id,
                Login = user.Login,
                Registered = user.Registered,
            });
        }
    }
}