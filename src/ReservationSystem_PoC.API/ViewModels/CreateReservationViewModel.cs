using System;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class CreateReservationViewModel : ContactBasicViewModel
    {
        public Guid? ContactId { get; set; }
        public string Message { get; set; }
    }
}
