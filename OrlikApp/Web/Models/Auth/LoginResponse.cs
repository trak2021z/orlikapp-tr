using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        public LoginResponse(BusinessLayer.Entities.User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            RoleName = user.Role.Name;
            Token = token;
        }
    }
}
