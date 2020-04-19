namespace MyCookbook.Web.ViewModels.Recipes.Create
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class RecipeCreateInputModel
    {
        [Display(Name = "Заглавие")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [StringLength(AttributesConstraints.RecipeTitleMaxLength, MinimumLength = AttributesConstraints.RecipeTitleMinLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Начин на приготвяне")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [StringLength(AttributesConstraints.RecipeDescriptionMaxLength, MinimumLength = AttributesConstraints.RecipeDescriptionMinLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Съвети")]
        [StringLength(AttributesConstraints.RecipeAdviceMaxLength, MinimumLength = AttributesConstraints.RecipeAdviceMinLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        public string Advices { get; set; }

        [Display(Name = "Порции")]
        [Range(AttributesConstraints.RecipeServingsMinValue, AttributesConstraints.RecipeServingsMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int Servings { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Време за подготовка")]
        [Range(AttributesConstraints.RecipePrepTimeMinValue, AttributesConstraints.RecipePrepTimeMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int PrepTime { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Време за готвене")]
        [Range(AttributesConstraints.RecipeCookTimeMinValue, AttributesConstraints.RecipeCookTimeMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CookTime { get; set; }

        [Display(Name = "Сезонен тип")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public Seasonal SeasonalType { get; set; }

        [Display(Name = "Ниво на трудност")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public SkillLevel SkillLevel { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CategoryId { get; set; }

        public IEnumerable<RecipeCreateCategoryDropDownViewModel> Categories { get; set; }

        [Display(Name = "Национална кухня")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CuisineId { get; set; }

        public IEnumerable<RecipeCreateCuisineDropDownViewModel> Cuisines { get; set; }

        [Display(Name = "Снимки")]
        [DataType(DataType.Upload)]
        [MaxCountElements(AttributesConstraints.RecipeImagesMaxCount)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public IEnumerable<IFormFile> Images { get; set; }

        [Display(Name = "Заглавна снимка")]
        [DataType(DataType.Upload)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", "png" })]
        public IFormFile TitleImage { get; set; }

        [Display(Name = "Съставки")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [StringLength(AttributesConstraints.IngredientsNamesMaxLength, MinimumLength = AttributesConstraints.IngredientsNamesMinLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        public string IngredientsNames { get; set; }

        [Display(Name = "Метод на приготвяне")]
        public RecipeCreateCookingMethodsCheckboxViewModel[] CookingMethods { get; set; }
    }
}
