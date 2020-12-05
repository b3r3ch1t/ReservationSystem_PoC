using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Domain.Core.IoC;

namespace ReservationSystem_PoC.Common.IoC
{
    internal static class DependencyResolverFaker
    {
        public static DependencyResolver GetDependencyResolver()
        {
            var services = new ServiceCollection();

            var provider = services.BuildServiceProvider();

            var dependencyResolver = new DependencyResolver(provider);

            return dependencyResolver;

        }
    }
}
