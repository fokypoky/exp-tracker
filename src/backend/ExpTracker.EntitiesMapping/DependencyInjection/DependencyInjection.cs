using ExpTracker.EntitiesMapping.Categories;
using ExpTracker.EntitiesMapping.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace ExpTracker.EntitiesMapping.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(DependencyInjection).Assembly);

            services.AddScoped<ITransactionCategoriesMapper, TransactionCategoriesMapper>();
            services.AddScoped<ITransactionsMapper, TransactionsMapper>();
        }
    }
}
