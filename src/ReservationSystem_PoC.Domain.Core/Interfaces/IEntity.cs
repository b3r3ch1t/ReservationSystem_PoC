using FluentValidation.Results;
using System;

namespace ReservationSystem_PoC.Domain.Core.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool Valid { get; }
        DateTime DateOfChange { get; }
        DateTime DateOfCreation { get; }


        ValidationResult ValidationResult { get; }
        void ChangeDateOfChange();
        void ChangeDateOfCreation();

        void Ativate();

        void ChangeId();

        bool IsValid();

        void Remove();
    }
}
