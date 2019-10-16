using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Field
{
    public class FieldSearch
    {
        public string Street { get; set; }
        public int? StreetNumber { get; set; }

        public FieldSearch(string street, int? streetNumber)
        {
            Street = street;
            StreetNumber = streetNumber;
        }
    }
}
