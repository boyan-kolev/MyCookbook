namespace MyCookbook.Web.ViewModels.Administration.Cuisines.Create
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class CuisinesCreateInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Име на националната кухня")]
        [StringLength(
            AttributesConstraints.CuisineNameMaxLength,
            MinimumLength = AttributesConstraints.CuisineNameMinLength,
            ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public string Name { get; set; }

        [Display(Name = "Снимка")]
        [DataType(DataType.Upload)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public IFormFile ImageUrl { get; set; }
    }
}
