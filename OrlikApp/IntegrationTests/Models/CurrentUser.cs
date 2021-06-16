using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Auth;

namespace IntegrationTests.Models
{
    public class CurrentUser
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public CurrentUser()
        {

        }

        public CurrentUser(LoginResponse loginResponse)
        {
            Id = loginResponse.Id;
            Email = loginResponse.Email;
            Name = loginResponse.Name;
        }
    }
}
