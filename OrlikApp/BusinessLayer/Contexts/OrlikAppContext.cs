using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Entities;
using BusinessLayer.Models.Enums;
using System.Linq;

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
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchMember> MatchMembers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>()
                .HasIndex(p =>  p.Email)
                .IsUnique(true);
            modelBuilder.Entity<User>()
                .HasIndex(p => p.Login)
                .IsUnique(true);
            
            modelBuilder.Entity<MatchMember>()
                .HasKey(mm => new { mm.MatchId, mm.PlayerId });

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = (long)RoleIds.Admin, Name = "Admin" },
                    new Role { Id = (long)RoleIds.User, Name = "Zawodnik" },
                    new Role { Id = (long)RoleIds.FieldManager, Name = "Kierownik boiska" }
                );

            modelBuilder.Entity<FieldType>()
                .HasData(
                    new FieldType { Id = (long)FieldTypeIds.ArtificialTurf, Name = "Sztuczna murawa" },
                    new FieldType { Id = (long)FieldTypeIds.Grass, Name = "Trawa" },
                    new FieldType { Id = (long)FieldTypeIds.Tartan, Name = "Tartan" },
                    new FieldType { Id = (long)FieldTypeIds.Asphalt, Name = "Asfalt" }
                );
        }
    }
}
