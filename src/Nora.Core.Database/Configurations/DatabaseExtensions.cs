using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Nora.Core.Database.Configurations;

public static class DatabaseExtensions
{
    public static IServiceCollection AddRepositories<TImplementation>(this IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblyOf<TImplementation>()
                .AddClasses(x => x.Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ImplementationType))
                .AsMatchingInterface()
                .WithScopedLifetime();
        });

        return services;
    }
}