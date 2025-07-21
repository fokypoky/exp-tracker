using ExpTracker.Core.Implementation;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;

namespace ExpTracker.Api.Extensions
{
	public static class ServicesExtension
	{
		private static AuthOptions CreateAuthOptions(WebApplicationBuilder builder)
		{
			return builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>() ??
			       throw new ArgumentNullException(paramName: "Auth options config section", message: "required");
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
