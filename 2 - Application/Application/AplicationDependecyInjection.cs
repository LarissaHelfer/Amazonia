using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.Contains("Application"))
                {
                    var handlers = assembly.ExportedTypes
                        .Where(type => type.Name.Contains("Handler") && type.IsClass && !type.IsAbstract)
                        .ToList();

                    handlers.ForEach(handler =>
                    {
                        var interfaceType = handler.GetInterfaces().FirstOrDefault(i => i.Name == $"I{handler.Name}");

                        if (interfaceType != null)
                        {
                            services.TryAddScoped(interfaceType, handler);
                        }
                        else
                        {
                            services.TryAddScoped(handler, handler);
                        }
                    });
                }
            }

            services.AddAutoMapper(assemblies);

            return services;
        }
    }
}
