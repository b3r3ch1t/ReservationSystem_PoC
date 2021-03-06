﻿using ReservationSystem_PoC.Domain.Core.Validators;
using System;
using System.Collections.Generic;

namespace ReservationSystem_PoC.Domain.Core.Entities
{
    public class Contact : EntityBase<Contact>
    {
        public const int MinNameSize = 3;
        public const int MaxNameSize = 255;

        public string Name { get; protected set; }

        public string PhoneNumber { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public ContactType ContactType { get; protected set; }
        public Guid ContactTypeId { get; protected set; }

        public IList<Reservation> Reservations { get; protected set; }

        protected Contact()
        {
            Reservations = new List<Reservation>();

        }

        public Contact(
            string name,
            string phoneNumber,
            DateTime birthDate,
            ContactType contactType)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;


            if (contactType != null) ContactTypeId = contactType.Id;

            Reservations = new List<Reservation>();
        }

        public override void Validate()
        {
            ValidationResult = new ContactValidator().Validate(this);
        }

        public static class Factory
        {
            public static Contact GetContact(
                string name,
                string phoneNumber,
                DateTime birthDate,
                ContactType contactType,
                IList<Reservation> reservations)
            {


                var contact = new Contact
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    BirthDate = birthDate,
                    ContactType = contactType,
                    ContactTypeId = contactType.Id,
                    Reservations = reservations
                };

                return contact;

            }

        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void ChangeBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
        }


        public void ChangeContactType(ContactType contactType)
        {
            if (contactType == null) return;

            ContactType = contactType;
            ContactTypeId = contactType.Id;
        }
    }
}
