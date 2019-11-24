using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Field
{
    public class FieldBaseItem
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public long TypeId { get; set; }
        public string Address { get; set; }
    }
}
