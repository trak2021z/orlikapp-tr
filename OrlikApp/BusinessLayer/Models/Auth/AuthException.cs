using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Auth
{
    public class AuthException : Exception
    {
        public AuthError ErrorCode { get; set; }

        public AuthException(string message, AuthError errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
