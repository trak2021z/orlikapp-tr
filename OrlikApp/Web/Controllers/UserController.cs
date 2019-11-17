using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Enums;
using BusinessLayer.Models.Role;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Helpers.Pagination;
using Web.Models.Helpers;
using Web.Models.User;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region List()
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPagedList([FromQuery]string username,
                                                     [FromQuery]string role, 
                                                     [FromQuery]int page, 
                                                     [FromQuery]int size)
        {
            var pager = new Pager(page, size);
            var filter = new UserSearch(username, role);

            var pagedDbUsers = await _userRepository.GetPagedList(filter, pager);
            var pagedResult = new PagedResult<UserListItem>
            {
                Items = _mapper.Map<IEnumerable<UserListItem>>(pagedDbUsers.Items),
                RowNumber = pagedDbUsers.RowNumber
            };

            return Ok(pagedResult);
        }
        #endregion

        #region  GetDetails()
        [HttpGet("{id:long}")]
        public async Task<ActionResult> GetDetails(long id)
        {
            var user = await _userRepository.GetWithRole(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDetailsResponse>(user));
        }
        #endregion

        #region Add()
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]UserCreateRequest request)
        {
            try
            {
                var createdDbUser = await _userRepository.Create(_mapper.Map<User>(request), request.Password);
                return CreatedAtAction("GetDetails",
                    new { createdDbUser.Id },
                    new { createdDbUser.Id, createdDbUser.Login });
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Edit()
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Edit(long id, [FromBody]UserBaseRequest request)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var updatedDbUser = await _userRepository.Update(id, _mapper.Map<User>(request));
                var result = _mapper.Map<UserUpdateResponse>(updatedDbUser);

                return Ok(result);
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Delete()
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(long id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.Remove(user);
            return Ok();
        }
        #endregion
    }
}
