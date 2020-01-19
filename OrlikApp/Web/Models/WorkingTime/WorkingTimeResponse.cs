using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.WorkingTime
{
    public class WorkingTimeResponse
    {
        public int Day { get; set; }
        public string DayName { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
    }
}
