using ExpTracker.Api.Middlewares;

namespace ExpTracker.Api.Extensions
{
	public static class MiddlewaresExtension
	{
		public static WebApplication AddMiddlewares(this WebApplication app)
		{
			app.UseCors(config =>
			{
				config.AllowAnyHeader();
				config.AllowAnyOrigin();
				config.AllowAnyMethod();
			});

			app.UseMiddleware<ExceptionsMiddleware>();

			app.UseAuthentication();
			app.UseAuthorization();

			return app;
		}
	}
}
