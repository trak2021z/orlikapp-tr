using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Entities
{
    public class Role
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}
