using ReservationSystem_PoC.Domain.Core.Bus;
using System;

namespace ReservationSystem_PoC.Domain.Core.Commands
{
    public class UpdateRankingOfReservationCommand : Command
    {
        public Guid ReservationId { get; set; }
        public int Ranking { get; set; }

        public UpdateRankingOfReservationCommand(Guid reservationId, int ranking)
        {
            ReservationId = reservationId;
            Ranking = ranking;
        }
    }
}
