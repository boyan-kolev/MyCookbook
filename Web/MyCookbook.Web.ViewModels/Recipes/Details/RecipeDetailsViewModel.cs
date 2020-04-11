namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
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

        public int TotalTime => this.CookTime + this.PrepTime;

        public string SeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public RecipeDetailsUserViewModel Author { get; set; }

        public string CategoryName { get; set; }

        public RecipeDetailsCuisineViewModel Cuisine { get; set; }

        public RecipeDetailsImagesViewModel[] Images { get; set; }

        public IEnumerable<RecipeDetailsIngredientsViewModel> Ingredients { get; set; }

        public IEnumerable<RecipeDetailsCookingMethodsViewModel> RecipesCookingMethods { get; set; }
    }
}
