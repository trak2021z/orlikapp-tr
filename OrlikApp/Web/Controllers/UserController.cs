using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Enums;
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
        public async Task<ActionResult> GetPagedList([FromQuery]string username,
                                                     [FromQuery]string role, 
                                                     [FromQuery]int page, 
                                                     [FromQuery]int size)
        {
            var pager = new Pager(page, size);

            var filter = new UserFilter();
            filter.Login = username;
            if (role != null)
            {
                switch (role.ToLower())
                {
                    case "admin":
                        filter.RoleId = (int)RoleName.Admin;
                        break;
                    case "user":
                        filter.RoleId = (int)RoleName.User;
                        break;
                }
            }

            var pagedDbUsers = await _userRepository.GetPagedList(_mapper.Map<UserSearch>(filter), pager);
            var pagedResult = new PagedResult<UserListItem>
            {
                Items = _mapper.Map<IEnumerable<UserListItem>>(pagedDbUsers.Items),
                RowNumber = pagedDbUsers.RowNumber
            };

            return Ok(pagedResult);
        }
        #endregion

        #region  GetDetails()
        // GET: api/users/5
        [HttpGet("{id}")]
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
        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]UserCreateRequest request)
        {
            try
            {
                var result = await _userRepository.Create(_mapper.Map<User>(request), request.Password);
                return CreatedAtAction("GetDetails", new { result.Id }, result);
            }
            catch (UserException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Edit()
        // PUT: api/users
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]UserUpdateRequest request)
        {
            var user = await _userRepository.Get(request.Id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _userRepository.Update(_mapper.Map<User>(request));
                return CreatedAtAction("GetDetails", new { result.Id }, result);
            }
            catch (UserException e)
            {
                return BadRequest(_mapper.Map<BadRequestModel>(e));
            }
        }
        #endregion

        #region Delete()
        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(long id)
        {
            var user = await _userRepository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userRepository.Remove(user);
            return Ok(user);
        }
        #endregion
    }
}
