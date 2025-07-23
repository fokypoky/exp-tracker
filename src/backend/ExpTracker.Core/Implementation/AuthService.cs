using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using Microsoft.IdentityModel.Tokens;

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

		public async Task<ServiceResponse<JwtTokenPair>> Register(AuthRequest request)
		{
			var userResponse = await _usersService.GetByLoginAsync(request.Login);

			if (userResponse.Result == ResponseResult.Ok)
				return ServiceResponse<JwtTokenPair>.BadRequest($"{request.Login} already exists");

			var hash = HashPassword(request.Password);
			var guid = Guid.NewGuid();
			var claims = CreateClaims(guid.ToString(), request.Login);

			var tokenPair = CreateJwtTokenPair(claims);

			var createUserResponse = await _usersService.CreateAsync(guid, request.Login, hash, tokenPair.RefreshToken);

			if (createUserResponse.Result != ResponseResult.Ok)
				return new ServiceResponse<JwtTokenPair>() { Result = createUserResponse.Result, Error = createUserResponse.Error };

			return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
		}

		public async Task<ServiceResponse<JwtTokenPair>> LogIn(AuthRequest request)
		{
			var hash = HashPassword(request.Password);
			var userResponse = await _usersService.GetByLoginAndPasswordAsync(request.Login, hash);

			if (userResponse.Result != ResponseResult.Ok)
				return new ServiceResponse<JwtTokenPair>() { Result = userResponse.Result, Error = userResponse.Error };

			var tokenPair = CreateJwtTokenPair(CreateClaims(userResponse.Data!.Id.ToString(), request.Login));

			await _usersService.UpdateRefreshTokenAsync(userResponse.Data!, tokenPair.RefreshToken);

			return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
		}

		public async Task<ServiceResponse<bool>> LogOut(RefreshTokenRequest request)
		{
			var guid = GetGuidFromToken(request.RefreshToken);

			if (guid == null)
				return ServiceResponse<bool>.Unauthorized("Invalid payload. Expected guid claim");

			var userResponse = await _usersService.GetByIdAsync(guid.Value);

			if (userResponse.Result != ResponseResult.Ok)
				return new ServiceResponse<bool>() { Result = userResponse.Result, Error = userResponse.Error };

			var refreshToken = userResponse.Data!.RefreshToken ?? "";

			if (!refreshToken.Equals(request.RefreshToken))
				return ServiceResponse<bool>.Unauthorized("Invalid refresh token");

			await _usersService.UpdateRefreshTokenAsync(userResponse.Data!, null);

			return ServiceResponse<bool>.Created();
		}

		public async Task<ServiceResponse<JwtTokenPair>> RefreshToken(RefreshTokenRequest request)
		{
			if (IsTokenExpired(request.RefreshToken))
				return ServiceResponse<JwtTokenPair>.Unauthorized("Refresh token expired");

			var guid = GetGuidFromToken(request.RefreshToken);
			var login = GetLoginFromToken(request.RefreshToken);

			if (guid == null)
				return ServiceResponse<JwtTokenPair>.Unauthorized("Invalid payload. Expected guid claim");

			if (login == null)
				return ServiceResponse<JwtTokenPair>.Unauthorized("Invalid payload. Expected login claim");

			var userResponse = await _usersService.GetByIdAsync(guid.Value);

			if (userResponse.Result != ResponseResult.Ok)
				return new ServiceResponse<JwtTokenPair>() { Result = userResponse.Result, Error = userResponse.Error };

			var tokenPair = CreateJwtTokenPair(CreateClaims(guid.ToString()!, login));

			await _usersService.UpdateRefreshTokenAsync(userResponse.Data!, tokenPair.RefreshToken);

			return ServiceResponse<JwtTokenPair>.Ok(tokenPair);
		}

		private string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

				var builder = new StringBuilder();
				foreach (var b in bytes)
				{
					builder.Append(b.ToString("x2"));
				}

				return builder.ToString();
			}
		}

		private string CreateJwtToken(AuthOptions options, SigningCredentials key, DateTime expires, List<Claim> claims)
		{
			var jwt = new JwtSecurityToken(
				issuer: options.Issuer,
				claims: claims,
				expires: expires,
				signingCredentials: key
			);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		private List<Claim> CreateClaims(string guid, string login)
		{
			return new List<Claim>() { new("login", login), new("guid", guid) };
		}

		private JwtTokenPair CreateJwtTokenPair(List<Claim> claims)
		{
			var accessToken = CreateJwtToken(
				_authOptions,
				_authOptions.AccessTokenSigningKey,
				DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authOptions.AccessTokenExpireMinutes)),
				claims
			);

			var refreshToken = CreateJwtToken(
				_authOptions,
				_authOptions.RefreshTokenSigningKey,
				DateTime.UtcNow.Add(TimeSpan.FromDays(_authOptions.RefreshTokenExpireDays)),
				claims
			);

			return new JwtTokenPair() { AccessToken = accessToken, RefreshToken = refreshToken };
		}

		private List<Claim> GetTokenClaims(string token)
		{
			try
			{
				var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
				return jwt.Claims.ToList();
			}
			catch (Exception e)
			{
				return new List<Claim>();
			}
		}

		private Guid? GetGuidFromToken(string token)
		{
			return Guid.Parse(GetTokenClaims(token).FirstOrDefault(_ => _.Type == "guid").Value);
		}

		private string? GetLoginFromToken(string token)
		{
			return GetTokenClaims(token).FirstOrDefault(_ => _.Type == "login").Value;
		}

		private bool IsTokenExpired(string token)
		{
			try
			{
				var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
				return jwt.ValidTo <= DateTime.UtcNow;
			}
			catch (Exception e)
			{
				return true;
			}
		}
	}
}
