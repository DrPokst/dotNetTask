using System;
using System.ComponentModel.DataAnnotations;

namespace dotNetTask.API.Helpers
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private string DependentProperty { get; }
        public NotEqualAttribute(string dependentProperty)
        {
            if (string.IsNullOrEmpty(dependentProperty))
            {
            throw new ArgumentNullException(nameof(dependentProperty));
            }
            DependentProperty = dependentProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
        if (value is not null)
        {
            var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(DependentProperty);
            var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);
            if (value.Equals(otherPropertyValue))
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
        }
    }
}