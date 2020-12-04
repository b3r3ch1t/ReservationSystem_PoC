using ReservationSystem_PoC.Domain.Core.Validators;
using System;

namespace ReservationSystem_PoC.Domain.Core.Entities
{
    public class Reservation : EntityBase<Reservation>
    {
        public const int MinMessageSize = 3;
        public const int MaxMessageSize = 255;


        public const int MinRanking = 1;
        public const int MaxRanking = 5;


        public Contact Contact { get; protected set; }
        public Guid ContactId { get; protected set; }
        public string Message { get; protected set; }

        public int Ranking { get; protected set; }
        public bool Favorited { get; protected set; }


        protected Reservation()
        {

        }

        public Reservation(
            string message,
            Contact contact,
            int ranking,
            bool favorited)
        {
            Message = message;
            Contact = contact;
            Ranking = ranking;
            Favorited = favorited;

            if (contact != null) ContactId = contact.Id;
        }


        public override void Validate()
        {
            ValidationResult = new ReservationValidator().Validate(this);
        }

        public void ChangeMessage(string message)
        {
            Message = message;
        }

        public void ChangeRanking(in int ranking)
        {
            Ranking = ranking;
        }

        public void ChangeFavorited()
        {
            Favorited = !Favorited;
        }
    }
}
