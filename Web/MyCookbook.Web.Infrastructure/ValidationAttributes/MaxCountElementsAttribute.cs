namespace MyCookbook.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class MaxCountElementsAttribute : ValidationAttribute
    {
        private readonly int maxCountElements;

        public MaxCountElementsAttribute(int maxCountFiles)
        {
            this.maxCountElements = maxCountFiles;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IEnumerable<IFormFile>;

            if (files.Count() > this.maxCountElements)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
