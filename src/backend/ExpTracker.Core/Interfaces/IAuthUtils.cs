using System.Security.Claims;
using ExpTracker.Entities.Dto.Responses.Auth;
using Microsoft.IdentityModel.Tokens;

namespace ExpTracker.Core.Interfaces
{
	public interface IAuthUtils
	{
		string HashPassword(string password);

		string CreateJwtToken(SigningCredentials key, DateTime expires, List<Claim> claims);
		JwtTokenPair CreateJwtTokenPair(List<Claim> claims);

		List<Claim> CreateClaims(string guid, string login);
		List<Claim> GetTokenClaims(string token);

		Guid? GetGuidFromToken(string token);
		string? GetLoginFromToken(string token);
		bool IsTokenExpired(string token);
	}
}
