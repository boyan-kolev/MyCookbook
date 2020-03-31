﻿namespace MyCookbook.Web.Infrastructure.ValidationAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IEnumerable<IFormFile>;

            if (files != null)
            {
                bool isValidSize = true;

                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.Length > this.maxFileSize)
                        {
                            isValidSize = false;
                            break;
                        }
                    }
                }

                if (isValidSize)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(this.ErrorMessage);
                }
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
