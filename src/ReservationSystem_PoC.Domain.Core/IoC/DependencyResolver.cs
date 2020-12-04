using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using System;

namespace ReservationSystem_PoC.Domain.Core.IoC
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TDependencyType Resolve<TDependencyType>()
        {
            return _serviceProvider.GetRequiredService<TDependencyType>();
        }

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}
