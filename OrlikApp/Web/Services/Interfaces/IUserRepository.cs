using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Entities;

namespace Web.Services
{
    public interface IUserRepository
    {
        Task<User> GetAsync(long id);
        Task<IList<User>> GetListAsync();
    }
}
