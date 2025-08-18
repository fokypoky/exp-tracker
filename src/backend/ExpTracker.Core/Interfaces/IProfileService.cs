using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Responses.Profile;

namespace ExpTracker.Core.Interfaces
{
    public interface IProfileService
    {
        Task<ServiceResponse<ProfileResponse>> GetAsync(string login);
    }
}