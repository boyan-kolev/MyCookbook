namespace MyCookbook.Web.ViewModels.Recipes.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Web.ViewModels.Recipes.ViewModels;

    public class RecipeCreateInputModel
    {
        [DisplayName("Заглавие")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [StringLength(AttributesConstraints.RecipeTitleMaxLength, MinimumLength = AttributesConstraints.RecipeTitleMinLength, ErrorMessage = AttributesErrorMessages.StringLengthMessage)]
        public string Title { get; set; }

        [DisplayName("Съвети")]
        [StringLength(AttributesConstraints.RecipeAdviceMaxLength, MinimumLength = AttributesConstraints.RecipeAdviceMinLength, ErrorMessage = AttributesErrorMessages.StringLengthMessage)]
        public string Advices { get; set; }

        [DisplayName("Порции")]
        [Range(AttributesConstraints.RecipeServingsMinValue, AttributesConstraints.RecipeServingsMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int Servings { get; set; }

        [DisplayName("Време за подготовка")]
        [Range(AttributesConstraints.RecipePrepTimeMinValue, AttributesConstraints.RecipePrepTimeMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int PrepTime { get; set; }

        [DisplayName("Време за готвене")]
        [Range(AttributesConstraints.RecipeCookTimeMinValue, AttributesConstraints.RecipeCookTimeMaxValue, ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CookTime { get; set; }

        [DisplayName("Сезонен тип")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public Seasonal SeasonalType { get; set; }

        [DisplayName("Ниво на трудност")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public SkillLevel SkillLevel { get; set; }

        [DisplayName("Категория")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CuisineId { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<CookingStep> CookingSteps { get; set; }

        public virtual ICollection<RecipeCookingMethod> RecipesCookingMethods { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedBy { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoritedBy { get; set; }
    }
}
