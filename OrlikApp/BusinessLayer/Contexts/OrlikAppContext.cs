﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Entities;
using BusinessLayer.Models.Enums;

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
            modelBuilder.Entity<User>()
                .HasIndex(p => p.Login)
                .IsUnique(true);
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = (long)RoleName.Admin, Name = "Admin" },
                    new Role { Id = (long)RoleName.User, Name = "User" }
                );
        }
    }
}
