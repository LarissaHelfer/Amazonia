using API.Infra.SqlServer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using API.Infra.SqlServer.Context;

namespace API.Infra.SqlServer
{
    public static class MySqlDependencyInjection
    {
        public static IServiceCollection AddMySqlDependency(this IServiceCollection services)
        {
            services.TryAddScoped<IDbContext>(provider => provider.GetService<ApiServerContext>());

            return services;
        }
    }
}
