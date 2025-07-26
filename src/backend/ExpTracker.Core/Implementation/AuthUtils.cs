using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.Entities.Dto.Responses.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpTracker.Core.Implementation
{
	public class AuthUtils : IAuthUtils
	{
		private readonly AuthOptions _authOptions;

		public AuthUtils(AuthOptions authOptions)
		{
			_authOptions = authOptions;
		}

		public string HashPassword(string password)
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

		public string CreateJwtToken(SigningCredentials key, DateTime expires, List<Claim> claims)
		{
			var jwt = new JwtSecurityToken(
				issuer: _authOptions.Issuer,
				claims: claims,
				expires: expires,
				signingCredentials: key
			);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public JwtTokenPair CreateJwtTokenPair(List<Claim> claims)
		{
			var accessToken = CreateJwtToken(
				_authOptions.AccessTokenSigningKey,
				DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authOptions.AccessTokenExpireMinutes)),
				claims
			);

			var refreshToken = CreateJwtToken(
				_authOptions.RefreshTokenSigningKey,
				DateTime.UtcNow.Add(TimeSpan.FromDays(_authOptions.RefreshTokenExpireDays)),
				claims
			);

			return new JwtTokenPair() { AccessToken = accessToken, RefreshToken = refreshToken };
		}

		public List<Claim> CreateClaims(string guid, string login)
		{
			return new List<Claim>() { new("login", login), new("guid", guid) };
		}

		public List<Claim> GetTokenClaims(string token)
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

		public Guid? GetGuidFromToken(string token)
		{
			return Guid.Parse(GetTokenClaims(token).FirstOrDefault(_ => _.Type == "guid").Value);
		}

		public string? GetLoginFromToken(string token)
		{
			return GetTokenClaims(token).FirstOrDefault(_ => _.Type == "login").Value;
		}

		public bool IsTokenExpired(string token)
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
