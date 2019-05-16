using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Helpers.Pagination;
using Web.Models.Helpers;
using Web.Models.Mapping;
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

        // POST: api/users
        [HttpPost("list")]
        public async Task<ActionResult> GetPagedList([FromBody] UserSearchRequest request)
        {
            request.Pager = new Pager()
            {
                Index = (request.Pager.Index <= 0) ? 1 : request.Pager.Index,
                Size = (request.Pager.Size <= 0) ? 10 : request.Pager.Size
            };

            var users = await _userRepository.GetPagedListAsync(_mapper.Map<UserSearch>(request));
            var pagedResult = new PagedResult<UserListItem>
            {
                Items = _mapper.Map<IEnumerable<UserListItem>>(users),
                RowNumber = request.Pager.RowNumber
            };

            return Ok(pagedResult);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDetails(long id)
        {
            var user = await _userRepository.GetWithRoleAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDetailsResponse>(user));
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]UserCreateRequest request)
        {
            try
            {
                var result = await _userRepository.Create(_mapper.Map<User>(request));

                return CreatedAtAction("GetDetails", new { id = result.Id }, result);
            }
            catch (UserException e)
            {
                return BadRequest(new BadRequestModel
                {
                    Message = e.Message,
                    ErrorCode = (int)e.ErrorCode
                });
            }
        }

        // PUT: api/users
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]UserUpdateRequest request)
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _userRepository.Update(_mapper.Map<User>(request));

                return CreatedAtAction("GetDetails", new { id = result.Id }, result);
            }
            catch (UserException e)
            {
                return BadRequest(new BadRequestModel
                {
                    Message = e.Message,
                    ErrorCode = (int)e.ErrorCode
                });
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(long id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userRepository.Remove(user);

            return Ok(user);
        }
    }
}
