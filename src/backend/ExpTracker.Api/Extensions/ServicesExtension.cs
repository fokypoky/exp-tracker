using System.Text;
using ExpTracker.Core.Implementation;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace ExpTracker.Api.Extensions
{
	public static class ServicesExtension
	{
		private static AuthOptions CreateAuthOptions(WebApplicationBuilder builder)
		{
			var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>() ??
			                  throw new ArgumentNullException(paramName: "Auth options config section", message: "required");

			authOptions.AccessTokenSigningKey = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.AccessTokenSecret)),
				SecurityAlgorithms.HmacSha256
			);

			authOptions.RefreshTokenSigningKey = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.RefreshTokenSecret)),
				SecurityAlgorithms.HmacSha256
			);

			return authOptions;
		}

		public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<AuthOptions>(_ => CreateAuthOptions(builder));

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IUsersService, UsersService>();

			return builder;
		}
	}
}
