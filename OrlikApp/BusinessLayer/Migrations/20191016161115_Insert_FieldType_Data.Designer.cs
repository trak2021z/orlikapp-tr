// <auto-generated />
using System;
using BusinessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessLayer.Migrations
{
    [DbContext(typeof(SRBContext))]
    [Migration("20191016161115_Insert_FieldType_Data")]
    partial class Insert_FieldType_Data
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessLayer.Entities.Field", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)");

                    b.Property<long?>("KeeperId");

                    b.Property<int?>("Length");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(120)");

                    b.Property<int?>("StreetNumber");

                    b.Property<long>("TypeId");

                    b.Property<int?>("Width");

                    b.HasKey("Id");

                    b.HasIndex("KeeperId");

                    b.HasIndex("TypeId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("BusinessLayer.Entities.FieldType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("FieldTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Sztuczna murawa"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Trawa"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Tartan"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Asfalt"
                        });
                });

            modelBuilder.Entity("BusinessLayer.Entities.Match", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descrition")
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("EndOfJoiningDate");

                    b.Property<long>("FieldId");

                    b.Property<long>("FounderId");

                    b.Property<bool>("IsAccepted");

                    b.Property<int?>("Minutes");

                    b.Property<int?>("PlayersAmmount");

                    b.Property<string>("Result");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("WantedPlayersAmmount");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("FounderId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("BusinessLayer.Entities.MatchMember", b =>
                {
                    b.Property<long>("MatchId");

                    b.Property<long>("PlayerId");

                    b.Property<DateTime>("JoiningDate");

                    b.HasKey("MatchId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchMembers");
                });

            modelBuilder.Entity("BusinessLayer.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("BusinessLayer.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(120)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("Height");

                    b.Property<bool?>("IsRightFooted");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("Number");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("RoleId");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(120)");

                    b.Property<int?>("StreetNumber");

                    b.Property<int?>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessLayer.Entities.WorkingTime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan?>("CloseHour")
                        .HasColumnType("time(0)");

                    b.Property<int>("Day");

                    b.Property<long?>("FieldId");

                    b.Property<TimeSpan?>("OpenHour")
                        .HasColumnType("time(0)");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.ToTable("WorkingTimes");
                });

            modelBuilder.Entity("BusinessLayer.Entities.Field", b =>
                {
                    b.HasOne("BusinessLayer.Entities.User", "Keeper")
                        .WithMany()
                        .HasForeignKey("KeeperId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessLayer.Entities.FieldType", "Type")
                        .WithMany("Fields")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusinessLayer.Entities.Match", b =>
                {
                    b.HasOne("BusinessLayer.Entities.Field", "Field")
                        .WithMany("Matches")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessLayer.Entities.User", "Founder")
                        .WithMany("FoundedMatches")
                        .HasForeignKey("FounderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusinessLayer.Entities.MatchMember", b =>
                {
                    b.HasOne("BusinessLayer.Entities.Match", "Match")
                        .WithMany("MatchMembers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessLayer.Entities.User", "Player")
                        .WithMany("MatchMembers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusinessLayer.Entities.User", b =>
                {
                    b.HasOne("BusinessLayer.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BusinessLayer.Entities.WorkingTime", b =>
                {
                    b.HasOne("BusinessLayer.Entities.Field", "Field")
                        .WithMany("WorkingTime")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
