using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Contexts;
using Web.Entities;

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
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IList<User>> GetListAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }
    }
}
