namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using System.Collections.Generic;

    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsViewModel : IMapFrom<RecipeDetailsServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Advices { get; set; }

        public int Servings { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public int TotalTime { get; set; }

        public int Cooked { get; set; }

        public double Ratings { get; set; }

        public int UsersStars { get; set; }

        public Seasonal SeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public RecipeDetailsUserViewModel Author { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public RecipeDetailsCuisineViewModel Cuisine { get; set; }

        public RecipeDetailsImagesViewModel[] Images { get; set; }

        public IEnumerable<RecipeDetailsIngredientsViewModel> Ingredients { get; set; }

        public IEnumerable<RecipeDetailsCookingMethodsViewModel> RecipesCookingMethods { get; set; }

        public IEnumerable<RecipeDetailsSimilarRecipesViewModel> SimilarRecipes { get; set; }
    }
}
