using System.ComponentModel.DataAnnotations;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class ContactTypeViewModel
    {

        [Display(Name = "Description of Contact")]
        public string Description { get; set; }
    }
}
