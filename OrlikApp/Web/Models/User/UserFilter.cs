﻿using BusinessLayer.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Models.User
{
    public class UserFilter
    {
        public string Login { get; set; }
        public long? RoleId { get; set; }
    }
}