﻿namespace MyCookbook.Web.ViewModels.Recipes.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;
    using MyCookbook.Web.ViewModels.Cuisines.ViewModels;
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

        [DisplayName("Национална кухня")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int CuisineId { get; set; }

        public IEnumerable<CuisineDropDownViewModel> Cuisines { get; set; }

        [DisplayName("Снимки")]
        [DataType(DataType.Upload)]
        [MaxCountElements(AttributesConstraints.RecipeImagesMaxCount)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", "png" })]
        public IEnumerable<IFormFile> Images { get; set; }

        [DisplayName("Заглавна снимка")]
        [DataType(DataType.Upload)]
        [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", "png" })]
        public IFormFile TitleImage { get; set; }

        [DisplayName("Съставки")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [StringLength(AttributesConstraints.IngredientsNamesMaxLength, MinimumLength = AttributesConstraints.IngredientsNamesMinLength, ErrorMessage = AttributesErrorMessages.StringLengthMessage)]
        public string IngredientsNames { get; set; }

        public virtual ICollection<RecipeCookingMethod> RecipesCookingMethods { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedBy { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoritedBy { get; set; }
    }
}