namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.CookingMethods;

    public class RecipeDetailsViewModel : IMapFrom<Recipe>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Advices { get; set; }

        public int Servings { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public string SeasonalType { get; set; }

        public string SkillLevel { get; set; }

        public string AuthorId { get; set; }

        public int CategoryId { get; set; }

        public int CuisineId { get; set; }

        public string TitlePhotoUrl { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        public IEnumerable<string> IngredientsName { get; set; }

        public IEnumerable<RecipeDetailsCookingMethodsViewModel> CookingMethods { get; set; }
    }
}
