using FluentValidation.Results;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using System;

namespace ReservationSystem_PoC.Domain.Core
{
    public abstract class EntityBase<T> : IEntity, IDisposable where T : IEntity
    {
        #region Properties
        public ValidationResult ValidationResult { get; protected set; }
        public Guid Id { get; set; }
        public bool Valid { get; private set; }
        public DateTime DateOfChange { get; private set; }
        public DateTime DateOfCreation { get; private set; }

        #endregion

        #region Construtor
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            DateOfCreation = DateTime.UtcNow;
            Valid = true;
            ValidationResult = new ValidationResult();
            DateOfChange = DateTime.UtcNow;
            DateOfCreation = DateTime.UtcNow;

        }
        #endregion

        #region Methods
        public bool IsValid()
        {
            try
            {
                Validate();
                return ValidationResult.IsValid;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            }

        }
        public void Remove()
        {
            Valid = false;
            ChangeDateOfChange();
        }
        public void ChangeDateOfChange()
        {
            DateOfChange = DateTime.UtcNow;
        }

        public void ChangeDateOfDateOfCreation()
        {
            DateOfCreation = DateTime.UtcNow;
        }

        public void Ativate()
        {
            Valid = true;

            ChangeDateOfChange();
        }

        public void ChangeId()
        {
            Id = Guid.NewGuid();
        }

        public abstract void Validate();

        #endregion

        #region Override

        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907);
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            return compareTo is { } && Id.Equals(compareTo.Id);
        }

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }

}
