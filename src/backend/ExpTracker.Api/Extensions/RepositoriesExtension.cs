using ExpTracker.DataAccess.PostgreSQL.Repositories.Implementation;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;

namespace ExpTracker.Api.Extensions
{
	public static class RepositoriesExtension
	{
		public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IUsersRepository, UsersRepository>();
			builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();

			return builder;
		}
	}
}
