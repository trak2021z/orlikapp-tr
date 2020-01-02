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

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
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

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(128)]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [MaxLength(128)]
        public string City { get; set; }

        public long RoleId { get; set; }

        public Role Role { get; set; }

        public List<Match> FoundedMatches { get; set; }

        public List<MatchMember> MatchMembers { get; set; }

        #region NOT MAPPED
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
        #endregion
    }
}
