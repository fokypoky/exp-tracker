using ExpTracker.Core.Implementation;
using ExpTracker.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ExpTracker.Core.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthUtils, AuthUtils>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
        }
    }
}
