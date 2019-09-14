using BusinessLayer.Entities;
using BusinessLayer.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(string login, string password);
        Task<User> RegisterUser(string login, string password, string email);
    }
}
