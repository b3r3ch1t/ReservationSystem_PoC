﻿using AutoMapper;
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

            CreateMap<Reservation, ReservationViewModel>()

                .ForMember(dest => dest.Message, o => o.MapFrom(map => map.Message))
                .ForMember(dest => dest.DateOfChange, o => o.MapFrom(map => map.DateOfChange))
                .ForMember(dest => dest.Ranking, o => o.MapFrom(map => map.Ranking))
                .ForMember(dest => dest.Favorited, o => o.MapFrom(map => map.Favorited))


                ;
        }
    }
}
