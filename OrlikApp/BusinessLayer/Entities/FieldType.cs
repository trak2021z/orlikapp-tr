using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLayer.Entities
{
    public class FieldType
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public List<Field> Fields { get; set; }
    }
}
