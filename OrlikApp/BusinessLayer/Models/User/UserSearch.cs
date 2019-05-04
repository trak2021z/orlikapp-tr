using BusinessLayer.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.User
{
    public class UserSearch
    {
        public Pager Pager { get; set; }
        public string Name { get; set; }
        public long? RoleId { get; set; }
    }
}
