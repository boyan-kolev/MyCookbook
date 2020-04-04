namespace MyCookbook.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    [AttributeUsage(AttributeTargets.Property)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public string GetErrorMessage()
        {
            return $"Файловете трябва да бъдат с размер до {this.maxFileSize}мб!";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IEnumerable<IFormFile>;

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.Length > this.maxFileSize)
                        {
                            return new ValidationResult(this.GetErrorMessage());
                        }
                    }
                }

                return ValidationResult.Success;
            }
            else
            {
                var file = value as IFormFile;

                if (file != null)
                {
                    if (file.Length > this.maxFileSize)
                    {
                        return new ValidationResult(this.ErrorMessage);
                    }
                }

                return ValidationResult.Success;
            }
        }
    }
}
