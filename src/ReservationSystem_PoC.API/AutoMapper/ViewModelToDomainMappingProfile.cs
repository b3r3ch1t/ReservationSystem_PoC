using AutoMapper;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Commands;
using System;

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
                    new DateTime(c.ContactBirthDateYear, c.ContactBirthDateMonth, c.ContactBirthDateDay),
                    c.ContactTypeId,
                    c.Message
                ));

            CreateMap<EditContactViewModel, EditContactCommand>()
                .ConstructUsing(c => new EditContactCommand(
                    c.ContactId,
                    c.ContactName,
                    c.ContactPhone,
                    new DateTime(c.ContactBirthDateYear, c.ContactBirthDateMonth, c.ContactBirthDateDay),
                    c.ContactTypeId
                ));

        }
    }
}
