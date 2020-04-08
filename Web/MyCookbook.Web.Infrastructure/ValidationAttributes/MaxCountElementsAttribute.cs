namespace MyCookbook.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    [AttributeUsage(AttributeTargets.Property)]
    public class MaxCountElementsAttribute : ValidationAttribute
    {
        private readonly int maxCountElements;

        public MaxCountElementsAttribute(int maxCountElements)
        {
            this.maxCountElements = maxCountElements;
        }

        public string GetErrorMessage()
        {
            return $"Можете да качите максимум {this.maxCountElements} файла!";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IEnumerable<IFormFile>;

            if (files != null)
            {
                if (files.Count() > this.maxCountElements)
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
    }
}
