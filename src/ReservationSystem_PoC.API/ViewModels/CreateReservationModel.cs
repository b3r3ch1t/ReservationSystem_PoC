﻿using System;

namespace ReservationSystem_PoC.API.ViewModels
{
    public class CreateReservationModel
    {

        public Guid? ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime ContactBirthdate { get; set; }
        public Guid ContactTypeId { get; set; }
    }
}
