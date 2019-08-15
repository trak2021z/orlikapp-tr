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
using Web.Helpers.Pagination;

namespace BusinessLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly OrlikAppContext _context;
        private readonly IHashService _hashService;

        public UserRepository(OrlikAppContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
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

        public async Task<PagedResult<User>> GetPagedListAsync(UserSearch search, Pager pager)
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            var query = users.AsEnumerable();

            if (!string.IsNullOrEmpty(search.Login))
            {
                query = query.Where(u => u.Login.Contains(search.Login));
            }

            if (search.RoleId != null)
            {
                query = query.Where(u => u.Role.Id == search.RoleId);
            }

            var resultList = query
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();

            var pagedList = new PagedResult<User>
            {
                Items = resultList.Skip(pager.Offset).Take(pager.Size),
                RowNumber = resultList.Count
            };

            return pagedList;
        }

        public async Task<User> Create(User user, string password)
        {
            try
            {
                await CheckUniqueFieldsAsync(user.Login, user.Email);

                user.DateCreated = DateTime.Now;
                user.DateModified = DateTime.Now;

                _hashService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

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
                await CheckUniqueFieldsAsync(user.Login, user.Email, user.Id);

                var existingUser = await GetAsync(user.Id);

                user.PasswordHash = existingUser.PasswordHash;
                user.PasswordSalt = existingUser.PasswordSalt;
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

        public async Task CheckUniqueFieldsAsync(string login, string email, long id = 0)
        {
            var emailExists = await _context.Users.AsNoTracking()
                    .AnyAsync(u => u.Email == email && u.Id != id);

            if (emailExists)
            {
                throw new UserException("Podany email jest już zajęty", UserError.EmailAlreadyExists);
            }

            var loginExists = await _context.Users.AsNoTracking()
                .AnyAsync(u => u.Login == login && u.Id != id);

            if (loginExists)
            {
                throw new UserException("Podana nazwa użytkownika jest już zajęta", UserError.EmailAlreadyExists);
            }
        }
    }
}
