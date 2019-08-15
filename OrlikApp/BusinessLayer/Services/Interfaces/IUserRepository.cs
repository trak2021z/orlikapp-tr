using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.User;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Get(long id);
        Task<User> GetWithRole(long id);
        Task<PagedResult<User>> GetPagedList(UserSearch search, Pager pager);
        Task<User> Create(User user, string password);
        Task<User> Update(User user);
        Task<User> Remove(User user);
        Task CheckUniqueFields(string login, string email, long id = 0);
    }
}
