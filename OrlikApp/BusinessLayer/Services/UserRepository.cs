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
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetWithRoleAsync(long id)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetPagedListAsync(UserSearch search)
        {
            var query = await _context.Users.AsNoTracking().Include(u => u.Role).ToListAsync();

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

        public async Task<User> Create(User user)
        {
            try
            {
                var existingEmail = await _context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingEmail != null)
                {
                    throw new UserException("Podany email jest już zajęty", UserError.EmailAlreadyExists);
                }

                user.DateCreated = DateTime.Now;
                user.DateModified = DateTime.Now;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<User> Update(User user)
        {
            try
            {
                var existingEmail = await _context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == user.Email && u.Id != user.Id);

                if (existingEmail != null)
                {
                    throw new UserException("Podany email jest już zajęty", UserError.EmailAlreadyExists);
                }

                var existingUser = await GetAsync(user.Id);

                user.Password = existingUser.Password;
                user.DateCreated = existingUser.DateCreated;
                user.DateModified = DateTime.Now;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<User> Remove(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
