using System.Security.Cryptography;
using ExpTracker.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExpTracker.Core.Tests
{
	internal static class StaticData
	{
		public static AuthOptions AuthOptions { get; } = new AuthOptions()
		{
			Issuer = "ExpTracker.Api",
			AccessTokenSecret = "ACCESS_TOKEN_DEV_ACCESS_TOKEN_DEV",
			RefreshTokenSecret = "REFRESH_TOKEN_DEV_REFRESH_TOKEN_DEV",
			AccessTokenExpireMinutes = 15,
			RefreshTokenExpireDays = 30,
			AccessTokenSigningKey = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ACCESS_TOKEN_DEV_ACCESS_TOKEN_DEV")),
				SecurityAlgorithms.HmacSha256
			),
			RefreshTokenSigningKey = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes("REFRESH_TOKEN_DEV_REFRESH_TOKEN_DEV")),
				SecurityAlgorithms.HmacSha256
			)
		};

		public static string HashPasswordSha256(string password)
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
	}
}
