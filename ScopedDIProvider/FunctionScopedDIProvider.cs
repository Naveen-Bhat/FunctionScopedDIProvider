using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ScopedDIProvider
{
    public static class FunctionScopedDIProvider
    {
        private static IServiceProvider serviceProvider;
        private static readonly object locker = new object();

        public static async Task Using(Func<IServiceProvider, Task> action)
        {
            if (serviceProvider == null)
            {
                lock (locker)
                {
                    if (serviceProvider == null)
                    {
                        serviceProvider = BuildServiceProvider();
                    }
                }
            }

            var scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // TODO: Do your registrations here.
            // Ex: services.AddScoped<IMyService, MyService>();

            return services.BuildServiceProvider();
        }
    }
}
