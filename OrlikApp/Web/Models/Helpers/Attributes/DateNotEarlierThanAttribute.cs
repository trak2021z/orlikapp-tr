using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Helpers.Attributes
{
    public class DateNotEarlierThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateNotEarlierThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var attributePropertyValue = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Właściwość z tą nazwą nie została znaleziona");
            }

            var argumentPropertyValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (attributePropertyValue < argumentPropertyValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
