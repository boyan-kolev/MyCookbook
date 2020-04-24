namespace MyCookbook.Web.ViewModels.Recipes.Edit
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class RecipeEditInputModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

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

        public IEnumerable<RecipeEditCategoryDropDownViewModel> Categories { get; set; }

        [Display(Name = "Национална кухня")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CuisineId { get; set; }

        public IEnumerable<RecipeEditCuisineDropDownViewModel> Cuisines { get; set; }

        [Display(Name = "снимки")]
        [DataType(DataType.Upload)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public ICollection<IFormFile> NewImages { get; set; }

        public ICollection<RecipeEditImagesViewModel> Images { get; set; }

        public ICollection<RecipeEditIngredientsViewModel> Ingredients { get; set; }

        [Display(Name = "Съставки")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [StringLength(AttributesConstraints.IngredientsNamesMaxLength, MinimumLength = AttributesConstraints.IngredientsNamesMinLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        public string IngredientsNames { get; set; }

        [Display(Name = "Метод на приготвяне")]
        public RecipeEditCookingMethodsCheckboxViewModel[] AllCookingMethods { get; set; }

        public ICollection<int> RecipeCookingMethodsIds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeEditInputModel>()
                .ForMember(
                opt => opt.RecipeCookingMethodsIds,
                dest => dest.MapFrom(x => x.RecipesCookingMethods.Select(c => c.CookingMethodId)))
                .ForMember(
                opt => opt.IngredientsNames,
                dest => dest.MapFrom(x => string.Join("\r\n", x.Ingredients.Select(c => c.Name))));
        }
    }
}
