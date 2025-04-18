using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nora.Core.Database.Contracts.Repositories;
using Nora.Core.Database.Contracts;

namespace Nora.Core.Database.Postgres.EntityFramework.Configurations;

public static class EntityFrameworkExtensions
{
    public static IServiceCollection AddEntityFramework<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration) where TDbContext : DbContext
    {
        services
            .AddDbContext<TDbContext>(configuration)
            .AddScoped<ISqlContext, SqlContext>(provider => new SqlContext(provider.GetRequiredService<TDbContext>()))
            .AddScoped<IUnitOfWork<ISqlContext>, UnitOfWork<ISqlContext>>();

        return services;
    }

    private static IServiceCollection AddDbContext<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection") ?? string.Empty)
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}
