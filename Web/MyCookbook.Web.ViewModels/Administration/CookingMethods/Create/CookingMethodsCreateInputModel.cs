namespace MyCookbook.Web.ViewModels.Administration.CookingMethods.Create
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class CookingMethodsCreateInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Име на начина на приготвяне")]
        [StringLength(
            AttributesConstraints.CookingMethodMaxLength,
            MinimumLength = AttributesConstraints.CookingMethodMinLength,
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
