using System.Text;
using ExpTracker.Core.DependencyInjection;
using ExpTracker.Core.Models;
using ExpTracker.EntitiesMapping.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(options =>
            {
				options.SwaggerDoc("v1", new OpenApiInfo()
                {
					Title = "ExpTracker service",
					Version = "1.0.0",
                });

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

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

			builder.Services.AddCoreServices();
			builder.Services.AddMapping();

			return builder;
		}
	}
}
