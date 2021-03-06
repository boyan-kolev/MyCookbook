﻿namespace MyCookbook.Web.ViewModels.Recipes.Filtered
{
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Search;

    public class RecipeFilteredInputDto : IMapFrom<RecipeFilteredInputModel>
    {
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public int CuisineId { get; set; }

        public int CookingMethodId { get; set; }

        public int PrepTime { get; set; }

        public bool IsCheckedPrepTime { get; set; }

        public int CookTime { get; set; }

        public bool IsCheckedCookTime { get; set; }

        public Seasonal SeasonalType { get; set; }

        public bool IsCheckedSeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public bool IsCheckedSkillLevel { get; set; }

        public SortedType SortedType { get; set; }

        public int Page { get; set; } = 1;

        public int? Take { get; set; } = null;

        public int Skip { get; set; } = 0;
    }
}
