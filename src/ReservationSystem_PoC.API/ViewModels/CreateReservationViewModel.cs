using System;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class CreateReservationViewModel
    {

        public Guid? ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime ContactBirthdate { get; set; }
        public Guid ContactTypeId { get; set; }
        public string Message { get; set; }
    }
}
