using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;

namespace ExpTracker.Core.Implementation
{
	public class AuthService : IAuthService
	{
		private readonly IUsersService _usersService;
		private readonly IAuthUtils _authUtils;

		public AuthService(IUsersService usersService, IAuthUtils authUtils)
		{
			_usersService = usersService;
			_authUtils = authUtils;
		}

		public async Task<ServiceResponse<JwtTokenPair>> Register(AuthRequest request)
		{
			var userResponse = await _usersService.GetByLoginAsync(request.Login);

			if (userResponse.Result == ResponseResult.Ok)
				return ServiceResponse<JwtTokenPair>.BadRequest($"{request.Login} already exists");

			var hash = _authUtils.HashPassword(request.Password);
			var guid = Guid.NewGuid();
			
			var tokenPair = _authUtils.CreateJwtTokenPair(_authUtils.CreateClaims(guid.ToString(), request.Login));

			var createUserResponse = await _usersService.CreateAsync(guid, request.Login, hash, tokenPair.RefreshToken);

			if (createUserResponse.Result != ResponseResult.Ok)
				return new ServiceResponse<JwtTokenPair>() { Result = createUserResponse.Result, Error = createUserResponse.Error };

			return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
		}

		public async Task<ServiceResponse<JwtTokenPair>> LogIn(AuthRequest request)
		{
			var hash = _authUtils.HashPassword(request.Password);
			var userResponse = await _usersService.GetByLoginAndPasswordAsync(request.Login, hash);

			if (userResponse.Result != ResponseResult.Ok)
				return new ServiceResponse<JwtTokenPair>() { Result = userResponse.Result, Error = userResponse.Error };

			var tokenPair = _authUtils.CreateJwtTokenPair(_authUtils.CreateClaims(userResponse.Data!.Id.ToString(), request.Login));

			await _usersService.UpdateRefreshTokenAsync(userResponse.Data!, tokenPair.RefreshToken);

			return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
		}

		public async Task<ServiceResponse<bool?>> LogOut(RefreshTokenRequest request)
		{
			var guid = _authUtils.GetGuidFromToken(request.RefreshToken);

			if (guid == null)
				return ServiceResponse<bool?>.Unauthorized("Invalid payload. Expected guid claim");

			var userResponse = await _usersService.GetByIdAsync(guid.Value);

			if (userResponse.Result != ResponseResult.Ok)
				return ServiceResponse<bool?>.Unauthorized("User not exists");

			var refreshToken = userResponse.Data!.RefreshToken ?? "";

			if (!refreshToken.Equals(request.RefreshToken))
				return ServiceResponse<bool?>.Unauthorized("Invalid refresh token");

			if(_authUtils.IsTokenExpired(refreshToken))
				return ServiceResponse<bool?>.Unauthorized("Refresh token expired");

			await _usersService.UpdateRefreshTokenAsync(userResponse.Data!, null);

			return ServiceResponse<bool?>.Created(null);
		}

		public async Task<ServiceResponse<JwtTokenPair>> RefreshToken(RefreshTokenRequest request)
		{
			if (_authUtils.IsTokenExpired(request.RefreshToken))
				return ServiceResponse<JwtTokenPair>.Unauthorized("Refresh token expired");

			var guid = _authUtils.GetGuidFromToken(request.RefreshToken);
			var login = _authUtils.GetLoginFromToken(request.RefreshToken);

			if (guid == null)
				return ServiceResponse<JwtTokenPair>.Unauthorized("Invalid payload. Expected guid claim");

			if (login == null)
				return ServiceResponse<JwtTokenPair>.Unauthorized("Invalid payload. Expected login claim");

			var userResponse = await _usersService.GetByIdAsync(guid.Value);

			if (userResponse.Result != ResponseResult.Ok)
				return ServiceResponse<JwtTokenPair>.Unauthorized("User not found");

			if (userResponse.Data!.RefreshToken != request.RefreshToken)
				return ServiceResponse<JwtTokenPair>.Unauthorized("Invalid refresh token");

			var tokenPair = _authUtils.CreateJwtTokenPair(_authUtils.CreateClaims(guid.ToString()!, login));

			await _usersService.UpdateRefreshTokenAsync(userResponse.Data!, tokenPair.RefreshToken);

			return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
		}
	}
}
