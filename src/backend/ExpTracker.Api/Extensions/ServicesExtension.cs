using System.Text;
using ExpTracker.Core.Implementation;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
			var authOptions = CreateAuthOptions(builder);

			builder.Services.AddAuthorization();
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// указывает, будет ли валидироваться издатель при валидации токена
						ValidateIssuer = true,
						// строка, представляющая издателя
						ValidIssuer = authOptions.Issuer,
						// будет ли валидироваться потребитель токена
						ValidateAudience = false,
						// будет ли валидироваться время существования
						ValidateLifetime = true,
						// установка ключа безопасности
						IssuerSigningKey =
							new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.AccessTokenSecret)),
						// валидация ключа безопасности
						ValidateIssuerSigningKey = true,
					};
				});

			builder.Services.AddSingleton<AuthOptions>(_ => CreateAuthOptions(builder));
			builder.Services.AddSingleton<IAuthUtils, AuthUtils>();

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IUsersService, UsersService>();
			builder.Services.AddScoped<IProfileService, ProfileService>();

			return builder;
		}
	}
}
