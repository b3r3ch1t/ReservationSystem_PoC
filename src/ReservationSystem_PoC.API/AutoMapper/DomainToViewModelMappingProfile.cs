using AutoMapper;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.API.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //Create the Maps from Domain to ViewModel
            CreateMap<ContactType, ContactTypeViewModel>();
        }
    }
}
