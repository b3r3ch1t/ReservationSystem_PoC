using AutoMapper;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.API.AutoMapper
{
    public class EntityToCommandsMappingProfile : Profile
    {
        //Create the  from Entity  to  DomainCommands
        public EntityToCommandsMappingProfile()
        {
            CreateMap<Reservation, UpdateRankingOfReservationCommand>()
                .ConstructUsing(c => new UpdateRankingOfReservationCommand(c.Id, c.Ranking));

        }
    }
}
