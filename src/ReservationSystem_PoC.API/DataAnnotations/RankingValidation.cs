using ReservationSystem_PoC.Domain.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem_PoC.API.DataAnnotations
{
    public class RankingValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                var rankingCandidate = (int)value;
                return rankingCandidate <= Reservation.MaxRanking && rankingCandidate >= Reservation.MinRanking;
            }
            catch
            {
                return false;
            }
        }

    }
}
