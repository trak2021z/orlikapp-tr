using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.WorkingTime;

namespace Web.Models.Field
{
    public class FieldItem : FieldBaseItem
    {
        public int? Length { get; set; }
        public int? Width { get; set; }
        public string Description { get; set; }
        public bool AutoConfirm { get; set; }
        public string KeeperName { get; set; }
        public List<WorkingTimeResponse> WorkingTime { get; set; }
    }
}
