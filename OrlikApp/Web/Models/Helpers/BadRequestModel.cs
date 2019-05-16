using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Helpers
{
    public class BadRequestModel
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}
