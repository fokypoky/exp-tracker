using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.Refresh
{
    public class RefreshTokenCommand : IRequest<ServiceResponse<JwtTokenPair>>
    {
        public RefreshTokenRequest Request { get; set; }

        public RefreshTokenCommand(RefreshTokenRequest request)
        {
            Request = request;
        }
    }
}
