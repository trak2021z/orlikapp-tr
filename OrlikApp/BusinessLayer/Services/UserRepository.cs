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

        #region Get()
        public async Task<User> Get(long id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
        #endregion

        #region GetWithRole()
        public async Task<User> GetWithRole(long id)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        #endregion

        #region GetPagedList()
        public async Task<PagedResult<User>> GetPagedList(UserSearch search, Pager pager)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Create()
        public async Task<User> Create(User user, string password)
        {
            await CheckUniqueFields(user.Login, user.Email);

            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;

            _hashService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        #endregion

        #region Update()
        public async Task<User> Update(long id, User user)
        {
            await CheckUniqueFields(user.Login, user.Email, id);

            var existingUser = await Get(id);

            user.Id = existingUser.Id;
            user.PasswordHash = existingUser.PasswordHash;
            user.PasswordSalt = existingUser.PasswordSalt;
            user.DateCreated = existingUser.DateCreated;
            user.DateModified = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
        #endregion

        #region Remove()
        public async Task<User> Remove(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        #endregion

        #region CheckUniqueFields()
        public async Task CheckUniqueFields(string login, string email, long id = 0)
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
                throw new UserException("Podana nazwa użytkownika jest już zajęta", UserError.LoginAlreadyExists);
            }
        }
        #endregion
    }
}
