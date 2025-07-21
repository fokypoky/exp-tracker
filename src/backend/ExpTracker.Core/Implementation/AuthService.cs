using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;

namespace ExpTracker.Core.Implementation
{
	public class AuthService : IAuthService
	{
		private readonly AuthOptions _authOptions;
		private readonly IUsersService _usersService;

		public AuthService(AuthOptions authOptions, IUsersService usersService)
		{
			_authOptions = authOptions;
			_usersService = usersService;
		}

		public Task<ServiceResponse<JwtTokenPair>> LogIn(AuthRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResponse<bool>> LogOut(RefreshTokenRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<ServiceResponse<JwtTokenPair>> Register(AuthRequest request)
		{
			var userResponse = await _usersService.GetUserByLoginAsync(request.Login);

			if (userResponse.Result == ResponseResult.Ok)
				return ServiceResponse<JwtTokenPair>.BadRequest($"{request.Login} already exists");


			return new ServiceResponse<JwtTokenPair>();
		}

		public Task<ServiceResponse<JwtTokenPair>> RefreshToken(RefreshTokenRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
