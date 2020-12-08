using Bogus;
using ReservationSystem_PoC.Common.Context;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.Entities;
using System.Linq;

namespace ReservationSystem_PoC.Common.Commands
{
    public static class UpdateRankingOfReservationCommandFaker
    {

        public static UpdateRankingOfReservationCommand UpdateRankingOfReservationCommandOk()
        {
            var faker = new Faker();

            var context = ReservarionSystemDbContextFaker.GetDatabaseInMemory();

            var reservation = faker.PickRandom<Reservation>(context.Reservations.ToList());

            var ranking = faker.Random.Int(min: Reservation.MinRanking, max: Reservation.MaxRanking);

            var result = new UpdateRankingOfReservationCommand(reservationId: reservation.Id,
                ranking: ranking);

            return result;

        }
    }
}
