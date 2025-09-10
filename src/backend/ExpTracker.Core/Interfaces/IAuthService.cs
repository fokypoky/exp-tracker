using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;

namespace ExpTracker.Core.Interfaces
{
	public interface IAuthService
	{
		Task<ServiceResponse<JwtTokenPair>> LogIn(AuthRequest request);
		Task<ServiceResponse<bool?>> LogOut(RefreshTokenRequest request);
		Task<ServiceResponse<JwtTokenPair>> Register(AuthRequest request);
		Task<ServiceResponse<JwtTokenPair>> RefreshToken(RefreshTokenRequest request);
	}
}
