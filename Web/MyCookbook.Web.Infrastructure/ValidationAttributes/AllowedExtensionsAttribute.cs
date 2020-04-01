namespace MyCookbook.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IEnumerable<IFormFile>;

            if (files != null)
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);

                    if (file != null)
                    {
                        if (!this.extensions.Contains(extension.ToLower()))
                        {
                            return new ValidationResult(this.ErrorMessage);
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
                    var extension = Path.GetExtension(file.FileName);

                    if (!this.extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult(this.ErrorMessage);
                    }
                }

                return ValidationResult.Success;
            }
        }
    }
}
