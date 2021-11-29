using System;
using System.ComponentModel.DataAnnotations;

namespace dotNetTask.API.Helpers
{
    public class AgeRangeAttribute : ValidationAttribute
    {
        private int _minAge;
        private int _maxAge;
        public AgeRangeAttribute(int minAge, int maxAge)
        {
            _maxAge = maxAge;
            _minAge = minAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minAge) < DateTime.Now && date.AddYears(_maxAge) > DateTime.Now;
            }
            return false;
        }
    }
}