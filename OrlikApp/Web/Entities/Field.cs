using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Enums;

namespace Web.Entities
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

        public Address Address { get; set; }

        public User Keeper { get; set; }

        // TODO: Godziny otwarcia, Zdjęcie, Com (co to?)


    }
}
