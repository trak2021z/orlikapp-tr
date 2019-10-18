using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Helpers
{
    public class TimeLaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public TimeLaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var attributePropertyValue = (TimeSpan)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Właściwość z tą nazwą nie została znaleziona");
            }

            var argumentPropertyValue = (TimeSpan)property.GetValue(validationContext.ObjectInstance);

            if (attributePropertyValue <= argumentPropertyValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
