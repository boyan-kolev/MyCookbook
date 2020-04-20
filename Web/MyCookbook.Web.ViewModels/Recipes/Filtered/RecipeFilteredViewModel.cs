namespace MyCookbook.Web.ViewModels.Recipes.Filtered
{
    using System.Collections.Generic;

    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Web.ViewModels.Recipes.Search;

    public class RecipeFilteredViewModel
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public RecipeFilteredCuisineViewModel Cuisine { get; set; }

        public string CookingMethod { get; set; }

        public int PrepTime { get; set; }

        public bool IsCheckedPrepTime { get; set; }

        public int CookTime { get; set; }

        public bool IsCheckedCookTime { get; set; }

        public Seasonal SeasonalType { get; set; }

        public bool IsCheckedSeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public bool IsCheckedSkillLevel { get; set; }

        public SortedType SortedType { get; set; }

        public IEnumerable<RecipeFilteredRecipesViewModel> FilteredRecipes { get; set; }
    }
}
