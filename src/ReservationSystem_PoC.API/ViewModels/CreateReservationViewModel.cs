using System;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class CreateReservationViewModel
    {

        public Guid? ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public int ContactBirthDateDay { get; set; }
        public int ContactBirthDateMonth { get; set; }
        public int ContactBirthDateYear { get; set; }
        public Guid ContactTypeId { get; set; }
        public string Message { get; set; }
    }
}
