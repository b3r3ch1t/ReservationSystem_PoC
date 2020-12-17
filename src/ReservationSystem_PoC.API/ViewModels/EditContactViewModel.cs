using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class EditContactViewModel : ContactBasicViewModel
    {


        [Required]
        public new Guid ContactId { get; set; }
    }
}
