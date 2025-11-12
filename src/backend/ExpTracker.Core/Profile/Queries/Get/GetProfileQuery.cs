using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Responses.Profile;
using MediatR;

namespace ExpTracker.Core.Profile.Queries.Get
{
    public class GetProfileQuery : IRequest<ServiceResponse<ProfileResponse>>
    {
        public string Login { get; set; }

        public GetProfileQuery(string login)
        {
            Login = login;
        }
    }
}
