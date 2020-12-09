using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class ContactTypeViewModel
    {

        [Display(Name = "Description of Contact")]
        public string ContactTypeName { get; set; }

        public Guid ContactTypeId { get; set; }
    }
}
