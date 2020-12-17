using MediatR;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using ReservationSystem_PoC.Domain.Core.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.DomainHandlers
{
    public class ReservationCommandHandler : CommandHandler,
        IRequestHandler<CreateReservationCommand, CommandResponse>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IContactTypeRepository _contactTypeRepository;

        public ReservationCommandHandler(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            _reservationRepository = dependencyResolver.Resolve<IReservationRepository>();
            _contactRepository = dependencyResolver.Resolve<IContactRepository>();
            _contactTypeRepository = dependencyResolver.Resolve<IContactTypeRepository>();
        }


        public async Task<CommandResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.ContactId.HasValue) return CommandResponse.Fail("Contact Invalid");

            var contact = await _contactRepository.GetByIdAsync(request.ContactId.Value);
            if (contact == null)
            {
                var contactCreated = CreateContact(request);
                if (contactCreated == null) return CommandResponse.Fail("Contact Invalid");
                contact = contactCreated;
            }

            var reservation = new Reservation(
               id: request.ReservationId,
                message: request.Message,
                contact: contact,
                ranking: 1,
                favorited: false
            );

            if (!reservation.IsValid())
            {
                foreach (var item in reservation.ValidationResult.Errors)
                {
                    DomainNotification.Fail(item.ErrorMessage);
                }

                return CommandResponse.Fail("Reservation invalid !");
            }


            await _reservationRepository.AddAsync(reservation);

            var result = await _reservationRepository.CommitAsync();

            return result.Success
                ? CommandResponse.Ok()
                : CommandResponse.Fail("Fail recording the register in database !");
        }

        private Contact CreateContact(CreateReservationCommand request)
        {
            var contactType = _contactTypeRepository.GetByIdAsync(request.ContactTypeId).GetAwaiter().GetResult();
            if (contactType == null)
            {
                MediatorHandler.NotifyDomainNotification(
                  DomainNotification.Fail("The contact type is invalid !"));

                return null;

            }


            var contact = new Contact(
                name: request.ContactName,
                phoneNumber: request.ContactPhone,
                birthDate: request.ContactBirthdate,
                contactType: contactType
            );

            if (!contact.IsValid())
            {
                foreach (var item in contact.ValidationResult.Errors)
                {
                    DomainNotification.Fail(item.ErrorMessage);
                }

                return null;
            }


            _contactRepository.AddAsync(contact);

            var result = _contactRepository.CommitAsync().GetAwaiter().GetResult();

            return result.Success ? contact : null;
        }
    }

}
