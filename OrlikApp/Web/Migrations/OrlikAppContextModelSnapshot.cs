﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Contexts;

namespace Web.Migrations
{
    [DbContext(typeof(OrlikAppContext))]
    partial class OrlikAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Web.Entities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(120)");

                    b.Property<int?>("StreetNumber");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Web.Entities.Field", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AddressId");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("KeeperId");

                    b.Property<int?>("Length");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Type");

                    b.Property<int?>("Width");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("KeeperId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Web.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Web.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Number");

                    b.Property<long?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Web.Entities.Field", b =>
                {
                    b.HasOne("Web.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Web.Entities.User", "Keeper")
                        .WithMany()
                        .HasForeignKey("KeeperId");
                });

            modelBuilder.Entity("Web.Entities.User", b =>
                {
                    b.HasOne("Web.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
