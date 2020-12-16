using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Entities;
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
        /// Update the Update Reservation Raking
        /// </summary>
        /// <param name="command"><see cref="ChangeRankingReservationModel"/></param>
        /// <returns><see cref="ReservationViewModel"/></returns>
        [HttpPost]
        [Route("api/v1/updatereservationRaking/")]
        public async Task<ActionResult<ReservationViewModel>> UpdateReservationRaking([FromBody] ChangeRankingReservationModel command)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ModelStateErrorResponseError();
            }

            var reservation = await _reservationRepository
                .GetByIdAsync(command.ReservationId)
                .ConfigureAwait(false);

            if (reservation == null)
            {
                NotifyError(message: "The reservation is invalid !");
                return ModelStateErrorResponseError();
            }

            var updateRankingOfReservationCommand = _mapper.Map<UpdateRankingOfReservationCommand>(reservation);

            var result = await Mediator.SendCommandAsync(updateRankingOfReservationCommand);


            var reservationResult = _mapper.Map<ReservationViewModel>(reservation);

            reservationResult.Ranking = command.NewRanking;

            return ResponsePost(
                nameof(UpdateReservationRaking),
                reservationResult);

        }


        /// <summary>
        /// Get Min value of Reservation Ranking.
        /// </summary>
        /// <returns>List of <see cref="int"/></returns>
        [HttpGet()]
        [Route("api/v1/GetMinValueOfReservationRanking/")]
        public ActionResult<int> GetMinValueOfReservationRanking()
        {

            var result = Reservation.MinRanking;

            return ResponseGet(result);
        }


        /// <summary>
        /// Get Max value of Reservation Ranking.
        /// </summary>
        /// <returns>List of <see cref="int"/></returns>
        [HttpGet()]
        [Route("api/v1/GetMaxValueOfReservationRanking/")]
        public ActionResult<int> GetMaxValueOfReservationRanking()
        {

            var result = Reservation.MaxRanking;

            return ResponseGet(result);
        }


        /// <summary>
        /// Create new Reservation
        /// </summary>
        /// <param name="Create Reservation Raking"><see cref="CreateReservationViewModel"/></param>
        /// <returns><see cref="ReservationViewModel"/></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ReservationViewModel>> CreateReservationRaking([FromBody] CreateReservationViewModel model)
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
                nameof(UpdateReservationRaking),
                returnModel);

        }


    }
}
