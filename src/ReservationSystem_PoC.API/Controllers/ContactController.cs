using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem_PoC.API.ViewModels;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

            var model = _contactRepository.GetAllDto() ;

            Console.WriteLine(model);

            var result =await  _mapper.ProjectTo < ContactViewModel >(model).ToListAsync( );
 
            return ResponseGet(result);
        }

    }
}
