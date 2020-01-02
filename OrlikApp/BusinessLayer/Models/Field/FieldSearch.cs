using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Field
{
    public class FieldSearch
    {
        public string City { get; private set; }
        public string Street { get; private set; }

        public FieldSearch(string city, string street)
        {
            City = city;
            Street = street;
        }
    }
}
