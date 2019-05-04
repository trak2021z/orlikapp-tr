using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Helpers.Pagination;
using Web.Models.User;

namespace Web.Controllers
{
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
        [HttpPost]
        public async Task<ActionResult> GetPagedList([FromBody] UserSearchRequest request)
        {
            request.Pager = new Pager()
            {
                Index = (request.Pager.Index <= 0) ? 1 : request.Pager.Index,
                Size = (request.Pager.Size <= 0) ? 10 : request.Pager.Size
            };

            var userQuery = await _userRepository.GetPagedListAsync(_mapper.Map<UserSearch>(request));
            var userResult = new PagedResult<UserListItem>
            {
                Items = _mapper.Map<IEnumerable<UserListItem>>(userQuery),
                RowNumber = request.Pager.RowNumber
            };

            return Ok(userResult);
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

        //// PUT: api/User/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(long id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/User
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        //// DELETE: api/User/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteUser(long id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}

        //private bool UserExists(long id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
