﻿using BusinessLayer.Helpers.Pagination;
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

        public UserSearch(string login, string role = null)
        {
            if (!string.IsNullOrEmpty(login))
            {
                Login = login;
            }

            if (!string.IsNullOrEmpty(role))
            {
                switch (role.ToLower())
                {
                    case "admin":
                        RoleId = (int)RoleIds.Admin;
                        break;
                    case "zawodnik":
                        RoleId = (int)RoleIds.User;
                        break;
                }
            }
        }
    }
}
