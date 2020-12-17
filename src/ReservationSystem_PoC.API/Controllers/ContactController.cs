using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        public ContactController(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            _mapper = dependencyResolver.Resolve<IMapper>();
            _contactRepository = dependencyResolver.Resolve<IContactRepository>();
        }

        /// <summary>
        /// Get all Contact Type.
        /// </summary>
        /// <returns>List of <see cref="ContactViewModel"/></returns>
        [HttpGet()]
        public async Task<ActionResult<List<ContactViewModel>>> GetAll()
        {

            var model = _contactRepository.GetAllDto()
                .OrderBy(x => x.ContactName);

            var result = await _mapper.ProjectTo<ContactViewModel>(model).ToListAsync();

            return ResponseGet(result);
        }




        /// <summary>
        /// Edit Contact
        /// </summary>
        /// <param name="Edit Contact"><see cref="EditContactViewModel"/></param>
        /// <returns><see cref="ReservationViewModel"/></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ContactViewModel>> EditContact([FromBody] EditContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var editContactCommand = _mapper.Map<EditContactCommand>(model);

            var result = await Mediator.SendCommandAsync(editContactCommand);

            if (result.Success)
            {
                await Mediator.NotifyDomainNotification(
                    DomainNotification.Success($" The contact to {model.ContactName} was edited with success !"));

            }

            var contact = await _contactRepository.GetContactById(model.ContactId);

            var returnModel = _mapper.Map<ContactViewModel>(contact);

            return ResponsePost(
                nameof(EditContact),
                returnModel);

        }
    }
}
