using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using MediatR;

namespace ExpTracker.Core.Auth.Commands.LogOut
{
    public class LogOutCommand : IRequest<ServiceResponse<bool?>>
    {
        public RefreshTokenRequest Request { get; set; }

        public LogOutCommand(RefreshTokenRequest request)
        {
            Request = request;
        }
    }
}
