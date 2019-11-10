using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Field
{
    public class FieldSearch
    {
        public string Street { get; private set; }
        public int? StreetNumber { get; private set; }

        public FieldSearch(string street, int? streetNumber)
        {
            Street = street;
            StreetNumber = streetNumber;
        }
    }
}
