using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.BussinessLayer.Entities
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
        [Column(TypeName = "nvarchar(120)")]
        public string Email { get; set; }
    }
}
