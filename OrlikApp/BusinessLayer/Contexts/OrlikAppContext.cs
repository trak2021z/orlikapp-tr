﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Entities;

namespace BusinessLayer.Contexts
{
    public class OrlikAppContext : DbContext
    {
        public OrlikAppContext(DbContextOptions<OrlikAppContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(p =>  p.Email)
                .IsUnique(true);
        }
    }
}