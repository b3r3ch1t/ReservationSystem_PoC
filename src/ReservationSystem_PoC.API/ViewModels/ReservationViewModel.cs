using System;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class ReservationViewModel
    {
        public string Message { get; set; }

        public DateTime DateOfChange { get; set; }

        public int Ranking { get; set; }

        public bool Favorited { get; set; }

    }
}
