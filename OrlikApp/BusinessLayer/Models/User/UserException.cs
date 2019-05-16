using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.User
{
    public class UserException : Exception
    {
        public UserError ErrorCode { get; set; }

        public UserException(string message, UserError errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
