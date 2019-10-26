using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.WorkingTime;

namespace Web.Models.Helpers
{
    public class ValidateWorkingTimeDaysAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var workingTimes = (IEnumerable<WorkingTimeRequest>)value;

            var uniqueDays = new List<int>();

            foreach (var workingTime in workingTimes)
            {
                if (!workingTime.Day.HasValue)
                {
                    return new ValidationResult(ErrorMessage);
                }

                if (workingTime.Day < 0 || workingTime.Day > 6)
                {
                    return new ValidationResult(ErrorMessage);
                }

                if (uniqueDays.Contains(workingTime.Day.Value))
                {
                    return new ValidationResult(ErrorMessage);
                }

                uniqueDays.Add(workingTime.Day.Value);
            }

            return ValidationResult.Success;
        }
    }
}
