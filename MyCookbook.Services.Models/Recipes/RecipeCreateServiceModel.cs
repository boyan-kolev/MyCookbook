namespace MyCookbook.Services.Models.Recipes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Web.ViewModels.CookingMethods;

    public class RecipeCreateServiceModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Advices { get; set; }

        public int Servings { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public Seasonal SeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public int CategoryId { get; set; }

        public int CuisineId { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IFormFile TitleImage { get; set; }

        public string IngredientsNames { get; set; }

        public CookingMethodsCheckboxViewModel[] CookingMethods { get; set; }
    }
}
