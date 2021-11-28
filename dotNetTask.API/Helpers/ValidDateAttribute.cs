using System;
using System.ComponentModel.DataAnnotations;

namespace dotNetTask.API.Helpers
{
    public class ValidDateAttribute : ValidationAttribute
    {
        private DateTime _minDate;
        private DateTime date;
        public ValidDateAttribute(string minDate)
        {
            _minDate = DateTime.Parse(minDate);
        } 

        public override bool IsValid(object value)
        {
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date <= DateTime.Now && date > _minDate;
            }
            return false;
        }
    }
}