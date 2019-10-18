using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Helpers
{
    public class BusinessLogicException : Exception
    {
        public int ErrorCode { get; set; }

        public BusinessLogicException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
