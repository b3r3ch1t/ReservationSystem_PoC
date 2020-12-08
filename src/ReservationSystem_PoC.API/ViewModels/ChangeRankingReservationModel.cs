using ReservationSystem_PoC.API.DataAnnotations;
using System;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class ChangeRankingReservationModel
    {
        public Guid ReservationId { get; set; }

        [RankingValidation(ErrorMessage = "The value of Ranking is invalid !")]
        public int NewRanking { get; set; }
    }
}
