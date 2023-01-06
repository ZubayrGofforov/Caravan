using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caravan.Service.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return new ValidationResult("Phone Number can not be null!");

            Regex regex = new Regex("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$");

            return regex.Match(value.ToString()!).Success ? ValidationResult.Success 
                : new ValidationResult("Please enter valid phone number. Phone must be contains only numbers or + character");
        }
    }
}
