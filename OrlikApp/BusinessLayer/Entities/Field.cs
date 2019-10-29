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

        public int? Length { get; set; }

        public int? Width { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string Street { get; set; }

        public int? StreetNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string City { get; set; }

        public bool AutoConfirm { get; set; }

        public long? KeeperId { get; set; }

        public User Keeper { get; set; }

        public long TypeId { get; set; }

        public FieldType Type { get; set; }

        public List<WorkingTime> WorkingTime { get; set; }

        public List<Match> Matches { get; set; }

        // TODO: Avatar


    }
}
