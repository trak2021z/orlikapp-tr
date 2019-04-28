﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities;

namespace Web.Contexts
{
    public class OrlikAppContext : DbContext
    {
        public OrlikAppContext(DbContextOptions<OrlikAppContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}