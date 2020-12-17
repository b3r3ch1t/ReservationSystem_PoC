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
    public class ContactCommandHandler : CommandHandler,
          IRequestHandler<EditContactCommand, CommandResponse>,
          IRequestHandler<DeleteContactCommand, CommandResponse>,
          IRequestHandler<CreateContactCommand, CommandResponse>
    {

        private readonly IContactRepository _contactRepository;

        private readonly IContactTypeRepository _contactTypeRepository;

        public ContactCommandHandler(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            _contactRepository = dependencyResolver.Resolve<IContactRepository>();
            _contactTypeRepository = dependencyResolver.Resolve<IContactTypeRepository>();
        }

        public async Task<CommandResponse> Handle(EditContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.ContactId);

            if (contact == null)
            {
                await MediatorHandler.NotifyDomainNotification(DomainNotification.Fail("The Contact is invalid !"));

                return CommandResponse.Fail();
            }

            var contactType = await _contactTypeRepository.GetByIdAsync(request.ContactTypeId);

            if (contactType == null)
            {
                await MediatorHandler.NotifyDomainNotification(DomainNotification.Fail("The Contact Type is invalid !"));

                return CommandResponse.Fail();
            }

            contact.ChangeName(request.ContactName);

            contact.ChangeBirthDate(request.ContactBirthDate);

            contact.ChangePhoneNumber(request.ContactPhone);

            contact.ChangeContactType(contactType);

            if (!contact.IsValid())
            {
                foreach (var item in contact.ValidationResult.Errors)
                {
                    await MediatorHandler.NotifyDomainNotification(DomainNotification.Fail(item.ErrorMessage));
                }

                return CommandResponse.Fail("Contact invalid !");
            }

            _contactRepository.Update(contact);

            var result = await _contactRepository.CommitAsync();

            return result.Success
                ? CommandResponse.Ok()
                : CommandResponse.Fail("Fail recording the register in database !");
        }

        public async Task<CommandResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.ContactId);

            if (contact == null)
            {
                await MediatorHandler.NotifyDomainNotification(DomainNotification.Fail("The Contact is invalid !"));

                return CommandResponse.Fail();
            }

            //the system does not exclude, just mark the Valid as false; 
            contact.Remove();

            _contactRepository.Update(contact);

            var result = await _contactRepository.CommitAsync();

            return result.Success
                ? CommandResponse.Ok()
                : CommandResponse.Fail("Fail recording the register in database !");
        }

        public async Task<CommandResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contactType = await _contactTypeRepository.GetByIdAsync(request.ContactTypeId);

            if (contactType == null)
            {
                await MediatorHandler.NotifyDomainNotification(DomainNotification.Fail("The Contact Id is not valid !"));
                return CommandResponse.Fail();
            }

            var contact = new Contact(
                name: request.ContactName,
                phoneNumber: request.ContactPhone,
                birthDate: request.ContactBirthDate,
                contactType: contactType
                );

            if (!contact.IsValid())
            {
                foreach (var item in contact.ValidationResult.Errors)
                {
                    await MediatorHandler.NotifyDomainNotification(DomainNotification.Fail(item.ErrorMessage));
                }

                return CommandResponse.Fail("Contact invalid !");
            }

            await _contactRepository.AddAsync(contact);

            var result = await _contactRepository.CommitAsync();

            return result.Success
                ? CommandResponse.Ok(contact.Id)
                : CommandResponse.Fail("Fail recording the register in database !");
        }
    }
}
