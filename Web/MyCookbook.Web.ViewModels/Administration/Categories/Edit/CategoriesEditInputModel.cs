namespace MyCookbook.Web.ViewModels.Administration.Categories.Edit
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class CategoriesEditInputModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Display(Name = "Име на категорията")]
        [StringLength(
            AttributesConstraints.CategoryNameMaxLength,
            MinimumLength = AttributesConstraints.CategoryNameMinLength,
            ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public string Name { get; set; }

        [Display(Name = "Снимка")]
        [DataType(DataType.Upload)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile NewImageUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}
