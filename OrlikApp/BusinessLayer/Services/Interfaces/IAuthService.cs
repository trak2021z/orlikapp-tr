using BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string login, string password);
        Task<User> RegisterUserAsync(string login, string password, string email);
    }
}
