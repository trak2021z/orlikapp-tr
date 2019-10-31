using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLayer.Models.Enums;

namespace BusinessLayer.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Login { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string City { get; set; }


        //TODO: Avatar

        public long RoleId { get; set; }

        public Role Role { get; set; }

        public List<Match> FoundedMatches { get; set; }

        public List<MatchMember> MatchMembers { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(LastName))
                {
                    return FirstName + " " + LastName;
                }
                return string.Empty;
            }
        }
    }
}
