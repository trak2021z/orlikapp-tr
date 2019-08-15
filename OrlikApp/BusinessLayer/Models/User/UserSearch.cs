using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.User
{
    public class UserSearch
    {
        public string Login { get; set; }
        public long? RoleId { get; set; }

        public UserSearch()
        {
                
        }

        public UserSearch(string login, string role)
        {
            Login = login;

            switch (role.ToLower())
            {
                case "admin":
                    RoleId = (int)RoleName.Admin;
                    break;
                case "user":
                    RoleId = (int)RoleName.User;
                    break;
            }
        }
    }
}
