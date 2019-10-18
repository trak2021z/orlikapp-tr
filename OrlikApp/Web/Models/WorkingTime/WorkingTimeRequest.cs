using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.WorkingTime
{
    public class WorkingTimeRequest
    {
        [Required(ErrorMessage = "Dzień jest wymagany")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Godzina jest wymagana")]
        public TimeSpan OpenHour { get; set; }

        [Required(ErrorMessage = "Godzina jest wymagana")]
        public TimeSpan CloseHour { get; set; }

        [Required(ErrorMessage = "Boisko jest wymagane")]
        public long FieldId { get; set; }
    }
}
