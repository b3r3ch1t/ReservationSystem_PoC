using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.API.AutoMapper;
using System;

namespace ReservationSystem_PoC.API.Configurations
{
    internal static class AutoMapperConfig
    {
        internal static void AddAutoMapperConfiguration(this IServiceCollection services)
        {

            //https://automapper.org/
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(
                typeof(DomainToViewModelMappingProfile),
                typeof(ViewModelToDomainMappingProfile),
                typeof(EntityToCommandsMappingProfile)
                );
        }
    }
}