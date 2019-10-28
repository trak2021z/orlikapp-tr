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
using Microsoft.Extensions.Logging;
using BusinessLayer.Helpers;
using BusinessLayer.Models.Field;
using BusinessLayer.Models.Role;

namespace BusinessLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly OrlikAppContext _context;
        private readonly IHashService _hashService;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(OrlikAppContext context, IHashService hashService, ILogger<UserRepository> logger)
        {
            _context = context;
            _hashService = hashService;
            _logger = logger;
        }

        #region Get()
        public async Task<User> Get(long id)
        {
            try
            {
                return await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetWithRole()
        public async Task<User> GetWithRole(long id)
        {
            try
            {
                return await _context.Set<User>().AsNoTracking().Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetPagedList()
        public async Task<PagedResult<User>> GetPagedList(UserSearch search, Pager pager)
        {
            try
            {
                var query = _context.Set<User>().AsNoTracking();

                if (!string.IsNullOrEmpty(search.Login))
                {
                    query = query.Where(u => u.Login.Contains(search.Login));
                }

                if (search.RoleId != null)
                {
                    query = query.Where(u => u.Role.Id == search.RoleId);
                }

                query = query.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                    .Skip(pager.Offset).Take(pager.Size);

                var queryResult = await query.ToListAsync();

                var result = new PagedResult<User>
                {
                    Items = queryResult,
                    RowNumber = queryResult.Count
                };

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Create()
        public async Task<User> Create(User user, string password)
        {
            try
            {
                await CheckUserUniqueFields(user.Login, user.Email);

                user.DateCreated = DateTime.UtcNow;
                user.DateModified = DateTime.UtcNow;

                _hashService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Update()
        public async Task<User> Update(long id, User user)
        {
            try
            {
                await CheckUserUniqueFields(user.Login, user.Email, id);

                var existingUser = await Get(id);

                user.Id = existingUser.Id;
                user.PasswordHash = existingUser.PasswordHash;
                user.PasswordSalt = existingUser.PasswordSalt;
                user.DateCreated = existingUser.DateCreated;
                user.DateModified = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Remove()
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
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region CheckUniqueFields()
        public async Task CheckUserUniqueFields(string login, string email, long id = 0)
        {
            var emailExists = await _context.Set<User>().AsNoTracking()
                    .AnyAsync(u => u.Email == email && u.Id != id);

            if (emailExists)
            {
                throw new BusinessLogicException("Podany email jest już zajęty",
                    (int)UserError.EmailAlreadyExists);
            }

            var loginExists = await _context.Users.AsNoTracking()
                .AnyAsync(u => u.Login == login && u.Id != id);

            if (loginExists)
            {
                throw new BusinessLogicException("Podana nazwa użytkownika jest już zajęta",
                    (int)UserError.LoginAlreadyExists);
            }
        }
        #endregion

        public async Task CheckKeeperPermission(long keeperId)
        {
            var keeper = await GetWithRole(keeperId);

            if (keeper.Role.Id == (long)RoleIds.User)
            {
                throw new BusinessLogicException("Podany opiekun nie ma odpowiedniej roli",
                    (int)FieldError.InvalidKeeperRole);
            }
        }
    }
}
