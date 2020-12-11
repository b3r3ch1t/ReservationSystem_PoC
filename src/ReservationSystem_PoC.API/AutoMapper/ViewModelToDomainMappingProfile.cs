using AutoMapper;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Commands;

namespace ReservationSystem_PoC.API.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //Create the  from ViewModel  to  Domain
            CreateMap<CreateReservationViewModel, CreateReservationCommand>()
                .ConstructUsing(c => new CreateReservationCommand(
                    c.ContactId,
                    c.ContactName,
                    c.ContactPhone,
                    c.ContactBirthdate,
                    c.ContactTypeId,
                    c.Message
                ));

            ;
        }
    }
}
