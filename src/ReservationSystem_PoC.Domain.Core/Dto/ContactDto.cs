using System;

namespace ReservationSystem_PoC.Domain.Core.Dto
{
    public class ContactDto
    {
        public Guid ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime ContactBirthdate { get; set; }
        public string ContactTypeName { get; set; }
        public Guid ContactTypeId { get; set; }
    }
}
