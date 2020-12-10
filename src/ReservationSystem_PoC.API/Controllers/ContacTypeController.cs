using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContacTypeController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IContactTypeRepository _contactTypeRepository;
        public ContacTypeController(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {

            _mapper = dependencyResolver.Resolve<IMapper>();
            _contactTypeRepository = dependencyResolver.Resolve<IContactTypeRepository>();
        }

        /// <summary>
        /// Get all Contact Type.
        /// </summary>
        /// <returns>List of <see cref="ContactTypeViewModel"/></returns>
        [HttpGet()]
        public async Task<ActionResult<List<ContactTypeViewModel>>> GetAll()
        {

            var model = _contactTypeRepository.GetAll();

            var result = await _mapper.ProjectTo<ContactTypeViewModel>(model).OrderBy(x => x.ContactTypeName).ToListAsync();

            return ResponseGet(result);
        }

    }
}
