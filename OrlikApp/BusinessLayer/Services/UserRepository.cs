using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.User;

namespace BusinessLayer.Services
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

        public async Task<IEnumerable<User>> GetPagedListAsync(UserSearch search)
        {
            var query = await _context.Users.Include(u => u.Role).ToListAsync();

            if (!String.IsNullOrEmpty(search.Name))
            {
                query = query.Where(u => u.Name.Contains(search.Name)).ToList();
            }

            if (search.RoleId != null)
            {
                query = query.Where(u => u.Role.Id == search.RoleId).ToList();
            }

            search.Pager.RowNumber = query.Count;

            return query
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Skip(search.Pager.Offset)
                .Take(search.Pager.Size);
        }
    }
}
