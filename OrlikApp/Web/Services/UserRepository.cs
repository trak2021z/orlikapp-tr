using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Contexts;
using Web.Entities;
using Web.Helpers;

namespace Web.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly OrlikAppContext _context;

        public UserRepository(OrlikAppContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetWithRoleAsync(long id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetPagedListAsync(Pager pager, long? roleId, string name = "")
        {
            var userEntity = await _context.Users.Include(u => u.Role).ToListAsync();

            if (!String.IsNullOrEmpty(name))
            {
                userEntity = userEntity.Where(u => u.Name.Contains(name)).ToList();
            }

            if (roleId != null)
            {
                userEntity = userEntity.Where(u => u.Role.Id == roleId).ToList();
            }

            return userEntity
                .OrderBy(u => u.Name)
                .Skip(pager.Offset)
                .Take(pager.Size);
        }
    }
}
