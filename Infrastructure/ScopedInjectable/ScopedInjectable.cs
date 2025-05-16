using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TaskManagement.ScopedInjectableExtension
{
    public static class DependencyInjectionExtensions
    {
        public static void AddScopedInjectables(this IServiceCollection services, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => typeof(IScopedInjectable).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in allTypes)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i != typeof(IScopedInjectable));

                foreach (var iface in interfaces)
                {
                    services.AddScoped(iface, type);
                }
            }
        }
    }
}
