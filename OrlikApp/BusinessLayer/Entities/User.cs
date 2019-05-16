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

        [Required]
        [Column(TypeName = "nvarchar(60)")]
        public string Password { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateModified { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public int? Number { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public bool? IsRightFooted { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string City { get; set; }


        //TODO: Avatar

        [Required]
        public long RoleId { get; set; }

        [Required]
        public Role Role { get; set; }

        [NotMapped]
        public string Name
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
