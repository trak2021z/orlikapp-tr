using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Auth
{
    public enum AuthError
    {
        InvalidLogin = 0,
        InvalidPassword = 1,
        EmptyToken = 2,
        EmptyPassword = 3
    }
}
