using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Models.Auth;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Auth;
using Web.Models.Helpers;

namespace Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginRequest request)
        {
            try
            {
                var token = _authService.Authenticate(request.Login, request.Password);
                return Ok(new { Token = token });
            }
            catch (AuthException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
    }
}