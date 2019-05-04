using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Models.User
{
    public class UserSearchRequest
    {
        public Pager Pager { get; set; }
        public string Name { get; set; }
        public long? RoleId { get; set; }
    }
}
