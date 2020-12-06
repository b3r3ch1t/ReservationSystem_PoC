using System;

namespace ReservationSystem_PoC.Domain.Core.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }


        void ChangeDateOfChange();
        void ChangeDateOfCreation();

        void Ativate();

        void ChangeId();
    }
}
