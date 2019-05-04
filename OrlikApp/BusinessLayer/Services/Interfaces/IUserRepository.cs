using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(long id);
        Task<User> GetWithRoleAsync(long id);
        Task<IEnumerable<User>> GetPagedListAsync(Pager pager, long? roleId, string name);
    }
}
