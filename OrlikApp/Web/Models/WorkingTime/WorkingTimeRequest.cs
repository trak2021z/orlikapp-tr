using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Helpers.Attributes;

namespace Web.Models.WorkingTime
{
    public class WorkingTimeRequest
    {
        [Required(ErrorMessage = "Dzień jest wymagany")]
        public int? Day { get; set; }

        [Required(ErrorMessage = "Godzina jest wymagana")]
        public TimeSpan? OpenHour { get; set; }

        [Required(ErrorMessage = "Godzina jest wymagana")]
        [TimeLaterThan("OpenHour", ErrorMessage = "Konflikt godziny otwarcia i zamknięcia")]
        public TimeSpan? CloseHour { get; set; }
    }
}
