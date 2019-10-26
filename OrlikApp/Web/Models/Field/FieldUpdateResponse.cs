using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.WorkingTime;

namespace Web.Models.Field
{
    public class FieldUpdateResponse
    {
        public int? Length { get; set; }
        public int? Width { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public string City { get; set; }
        public long? KeeperId { get; set; }
        public long TypeId { get; set; }
        public List<WorkingTimeResponse> WorkingTime { get; set; }
    }
}
