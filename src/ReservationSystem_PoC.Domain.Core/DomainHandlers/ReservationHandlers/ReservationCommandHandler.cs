using MediatR;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using ReservationSystem_PoC.Domain.Core.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.DomainHandlers.ReservationHandlers
{
    public class ReservationCommandHandler : CommandHandler,
        IRequestHandler<UpdateRankingOfReservationCommand, CommandResponse>,
        IRequestHandler<CreateReservationCommand, CommandResponse>
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationCommandHandler(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            _reservationRepository = dependencyResolver.Resolve<IReservationRepository>();
        }

        public async Task<CommandResponse> Handle(UpdateRankingOfReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation == null)
            {


                return CommandResponse.Fail("The reservation is invalid !");
            }

            reservation.ChangeRanking(request.Ranking);

            if (!reservation.IsValid())
            {

                foreach (var error in reservation.ValidationResult.Errors)
                {
                    await MediatorHandler.NotifyDomainNotification(
                          domainNotification: DomainNotification.Fail(error.ErrorMessage));
                }
                return CommandResponse.Fail("The reservation value is not  invalid !");
            }

            _reservationRepository.Update(reservation);

            var result = await _reservationRepository.CommitAsync();


            return result.Success ? CommandResponse.Ok() : CommandResponse.Fail();
        }

        public Task<CommandResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
