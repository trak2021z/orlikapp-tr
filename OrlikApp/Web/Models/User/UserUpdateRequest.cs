using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.User
{
    public class UserUpdateRequest : UserBaseRequest
    {
        public long Id { get; set; }
    }
}
