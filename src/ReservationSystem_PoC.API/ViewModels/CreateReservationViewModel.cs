using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class CreateReservationViewModel
    {

        public Guid? ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        [Range(1, 31)]
        public int ContactBirthDateDay { get; set; }


        [Range(1, 12)]
        public int ContactBirthDateMonth { get; set; }

        [Range(1, 2999)]
        public int ContactBirthDateYear { get; set; }
        public Guid ContactTypeId { get; set; }
        public string Message { get; set; }
    }
}
