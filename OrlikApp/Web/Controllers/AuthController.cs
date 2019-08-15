using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Models.Auth;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Auth;
using Web.Models.Helpers;

namespace Web.Controllers
{
    [Authorize]
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

        #region Login()
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginRequest request)
        {
            try
            {
                var token = await _authService.Authenticate(request.Login, request.Password);
                return Ok(new { Token = token });
            }
            catch (AuthException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Register()
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterRequest request)
        {
            try
            {
                var user = await _authService.RegisterUser(request.Login, request.Password, request.Email);
                return Ok(new { user.Id, user.Login });
            }
            catch (UserException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion
    }
}