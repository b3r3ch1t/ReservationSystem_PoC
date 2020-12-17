using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {

            _mapper = dependencyResolver.Resolve<IMapper>();
            _reservationRepository = dependencyResolver.Resolve<IReservationRepository>();
        }

        /// <summary>
        /// Get all Contact Type.
        /// </summary>
        /// <returns>List of <see cref="ReservationViewModel"/></returns>
        [HttpGet()]
        public async Task<ActionResult<List<ReservationViewModel>>> GetAll()
        {

            var model = _reservationRepository.GetAll();

            var result = await _mapper.ProjectTo<ReservationViewModel>(model).ToListAsync();

            return ResponseGet(result);
        }


        /// <summary>
        /// Create new Reservation
        /// </summary>
        /// <param name="model"><see cref="CreateReservationViewModel"/></param> 
        /// <returns><see cref="ReservationViewModel"/></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ReservationViewModel>> CreateReservation([FromBody] CreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            model.ContactId ??= Guid.NewGuid();

            var createReservationCommand = _mapper.Map<CreateReservationCommand>(model);

            var result = await Mediator.SendCommandAsync(createReservationCommand);

            if (result.Success)
            {
                await Mediator.NotifyDomainNotification(
                    DomainNotification.Success($" The reservation to {model.ContactName} was created with success !"));

            }

            var reservation = await _reservationRepository.GetByIdAsync(createReservationCommand.ReservationId);

            var returnModel = _mapper.Map<ReservationViewModel>(reservation);

            return ResponsePost(
                nameof(CreateReservation),
                returnModel);

        }



    }
}
