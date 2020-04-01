namespace MyCookbook.Web.ViewModels.Recipes.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class TestInputModel
    {
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = "Не е позволен този формат!")]
        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = "Не е позволено повече от 1мб!")]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
