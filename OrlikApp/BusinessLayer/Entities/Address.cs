using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Entities
{
    public class Address
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string City { get; set; }
    }
}
