using Helper.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var transients = typeof(Extensions).Assembly.ExportedTypes.Where(t => typeof(ITransient).IsAssignableFrom(t) && t.IsClass).ToList();
            foreach (var transient in transients)
            {
                var interfaceOfTransient = transient.GetInterface($"I{transient.Name}");
                services.AddTransient(interfaceOfTransient, transient);
            }
            var scopes = typeof(Extensions).Assembly.ExportedTypes.Where(t => typeof(IScoped).IsAssignableFrom(t) && t.IsClass).ToList();
            foreach (var scope in scopes)
            {
                var interfaceOfScoped = scope.GetInterface($"I{scope.Name}");
                services.AddScoped(interfaceOfScoped, scope);
            }
            var singletons = typeof(Extensions).Assembly.ExportedTypes.Where(t => typeof(ISingleton).IsAssignableFrom(t) && t.IsClass).ToList();
            foreach (var singleton in singletons)
            {
                var interfaceOfSingleton = singleton.GetInterface($"I{singleton.Name}");
                services.AddSingleton(interfaceOfSingleton, singleton);
            }
        }
    }
}
