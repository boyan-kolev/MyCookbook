namespace MyCookbook.Web.ViewModels.Recipes.Search
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Common;
    using MyCookbook.Data.Models.Enums;

    public class RecipeSearchInputModel
    {
        [Display(Name = "Заглавие")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [MaxLength(AttributesConstraints.RecipeTitleMaxLength, ErrorMessage = AttributesErrorMessages.MaxLengthErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public IEnumerable<RecipeSearchCategoryDropDownViewModel> Categories { get; set; }

        [Display(Name = "Национална кухня")]
        public int CuisineId { get; set; }

        public IEnumerable<RecipeSearchCuisineDropDownViewModel> Cuisines { get; set; }

        [Display(Name = "Метод на приготвяне")]
        public int CookingMethodId { get; set; }

        public IEnumerable<RecipeSearchCookingMethodsDropDownViewModel> CookingMethods { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Време за подготовка")]
        [Range(0, AttributesConstraints.RecipePrepTimeMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int PrepTime { get; set; }

        public bool IsCheckedPrepTime { get; set; }

        [DataType(DataType.Duration)]
        [Display(Name = "Време за готвене")]
        [Range(0, AttributesConstraints.RecipeCookTimeMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CookTime { get; set; }

        public bool IsCheckedCookTime { get; set; }

        [Display(Name = "Сезонен тип")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public Seasonal SeasonalType { get; set; }

        public bool IsCheckedSeasonalType { get; set; }

        [Display(Name = "Ниво на трудност")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public SkillLevel SkillLevel { get; set; }

        public bool IsCheckedSkillLevel { get; set; }

        [Display(Name = "Подреди по")]
        public SortedType SortedType { get; set; }
    }
}
