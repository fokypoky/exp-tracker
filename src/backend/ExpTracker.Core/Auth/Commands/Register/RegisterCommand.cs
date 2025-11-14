using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<ServiceResponse<JwtTokenPair>>
    {
        public AuthRequest Request { get; set; }

        public RegisterCommand(AuthRequest request)
        {
            Request = request;
        }
    }
}
