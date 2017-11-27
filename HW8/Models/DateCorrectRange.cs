using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW8.Models
{
        [AttributeUsage(AttributeTargets.Property)]
        public class DateCorrectRangeAttribute : ValidationAttribute
        {
            public bool ValidateBirthDate { get; set; }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var model = validationContext.ObjectInstance as Artist;

                if (model != null)
                {
                    if (model.BirthDate > DateTime.Now.Date)
                    {
                        return new ValidationResult(string.Empty);
                    }
                }
                return ValidationResult.Success;
            }
        }
    
}