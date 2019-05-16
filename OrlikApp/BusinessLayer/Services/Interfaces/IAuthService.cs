using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string login, string password);
    }
}
