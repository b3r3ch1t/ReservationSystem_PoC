namespace ReservationSystem_PoC.Domain.Core.Interfaces
{
    public interface IDependencyResolver
    {
        TDependencyType Resolve<TDependencyType>();
    }
}