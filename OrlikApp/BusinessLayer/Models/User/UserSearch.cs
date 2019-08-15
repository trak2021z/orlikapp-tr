using BusinessLayer.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.User
{
    public class UserSearch
    {
        public string Login { get; set; }
        public long? RoleId { get; set; }
    }
}
