using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLayer.Models.Enums;

namespace BusinessLayer.Entities
{
    public class Field
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        public string Name { get; set; }

        public int? Length { get; set; }

        public int? Width { get; set; }

        public FieldType Type { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string City { get; set; }



        [Required]
        public User Keeper { get; set; }

        public IEnumerable<WorkingTime> WorkingTime { get; set; }

        // TODO: Avatar


    }
}
