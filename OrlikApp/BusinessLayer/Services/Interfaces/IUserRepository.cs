using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(long id);
        Task<User> GetWithRoleAsync(long id);
        Task<IEnumerable<User>> GetPagedListAsync(UserSearch search);
        Task<User> Create(User user, string password);
        Task<User> Update(User user);
        Task<User> Remove(User user);
        Task CheckUniqueFieldsAsync(string login, string email, long id = 0);
    }
}
