using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _authService = authService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region Login()
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginRequest request)
        {
            try
            {
                var authResponse = await _authService.Authenticate(request.Login, request.Password);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    var userDetails = await _userRepository.GetWithRole(authResponse.UserId);
                    return Ok(new LoginResponse(userDetails, authResponse.Token));
                }

                return Unauthorized(new BadRequestModel()
                {
                    Message = "Pusty token",
                    ErrorCode = (int)AuthError.EmptyToken
                });
            }
            catch (BusinessLogicException e)
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
                var x = request.Email;
                var user = await _authService.RegisterUser(_mapper.Map<RegisterModel>(request));
                return Ok(new { user.Id, user.Login });
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion
    }
}