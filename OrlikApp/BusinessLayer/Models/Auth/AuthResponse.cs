using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Auth
{
    public class AuthResponse
    {
        public long UserId { get; set; }
        public string Token { get; set; }
    }
}
