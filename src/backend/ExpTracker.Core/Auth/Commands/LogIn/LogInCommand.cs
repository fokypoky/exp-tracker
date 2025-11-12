using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.LogIn
{
    public class LogInCommand : IRequest<ServiceResponse<JwtTokenPair>>
    {
        public AuthRequest Request { get; set; }

        public LogInCommand(AuthRequest request)
        {
            Request = request;
        }
    }
}
