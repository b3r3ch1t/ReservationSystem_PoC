using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Repositories;
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

    }
}
