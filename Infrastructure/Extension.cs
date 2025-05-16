using Domain.Queries;
using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagement.Application.Handlers.Queries;
using TaskManagement.Commands;
using TaskManagement.Queries;
using TaskManagement.ScopedInjectableExtension;

namespace TaskManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var readConnectionString = configuration.GetConnectionString("ReadConnection");
            var writeConnectionString = configuration.GetConnectionString("WriteConnection");

            services.AddDbContext<ReadDbContext>(options =>
                options.UseNpgsql(readConnectionString, npgsqlOptions =>
                    npgsqlOptions.EnableRetryOnFailure())
                    .UseLazyLoadingProxies().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContext<WriteDbContext>(options =>
                options.UseNpgsql(writeConnectionString, npgsqlOptions =>
                    npgsqlOptions.EnableRetryOnFailure()));

            services.AddScoped<IUnitOfWork, UnitOfWork<WriteDbContext>>();
            services.AddScopedInjectables(Assembly.GetExecutingAssembly());
            var applicationAssembly = typeof(GetAllProjectsHandler).Assembly;

            services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}
