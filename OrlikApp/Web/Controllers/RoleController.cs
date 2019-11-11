using AutoMapper;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Helpers;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        #region GetAll()
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var roles = await _roleRepository.GetAll();

            return Ok(_mapper.Map<IEnumerable<DictionaryModel>>(roles));
        }
        #endregion
    }
}
