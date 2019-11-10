using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Enums;
using BusinessLayer.Models.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.User
{
    public class UserSearch
    {
        public string Login { get; private set; }
        public long? RoleId { get; private set; }

        public UserSearch()
        {
                
        }

        public UserSearch(string login, string role = null)
        {
            if (!string.IsNullOrEmpty(login))
            {
                Login = login;
            }

            if (!string.IsNullOrEmpty(role))
            {
                switch (role.FirstLetterToUpper())
                {
                    case RoleNames.Admin:
                        RoleId = (long)RoleIds.Admin;
                        break;
                    case RoleNames.User:
                        RoleId = (long)RoleIds.User;
                        break;
                    case RoleNames.FieldKeeper:
                        RoleId = (long)RoleIds.FieldKeeper;
                        break;
                    default:
                        RoleId = -1;
                        break;
                }
            }
        }
    }
}
