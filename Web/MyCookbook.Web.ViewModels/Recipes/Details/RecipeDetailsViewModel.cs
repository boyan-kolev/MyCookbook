namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public bool IsApproved { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Advices { get; set; }

        public int Servings { get; set; }

        public int PrepTime { get; set; }

        public int CookTime { get; set; }

        public int TotalTime => this.CookTime + this.PrepTime;

        public int Cooked { get; set; }

        public double Ratings { get; set; }

        public RecipeDetailsUserViewModel User { get; set; }

        public Seasonal SeasonalType { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public RecipeDetailsAuthorViewModel Author { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public RecipeDetailsCuisineViewModel Cuisine { get; set; }

        public RecipeDetailsImagesViewModel[] Images { get; set; }

        public IEnumerable<RecipeDetailsIngredientsViewModel> Ingredients { get; set; }

        public IEnumerable<RecipeDetailsCookingMethodsViewModel> RecipesCookingMethods { get; set; }

        public IEnumerable<RecipeDetailsSimilarRecipesViewModel> SimilarRecipes { get; set; }

        public IEnumerable<RecipeDetailsCommentsViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeDetailsViewModel>()
                .ForMember(
                    dest => dest.Cooked,
                    opt => opt.MapFrom(x => x.CookedBy.Count))
                .ForMember(
                    dest => dest.Ratings,
                    opt => opt.MapFrom(x => x.Ratings.Average(r => r.Stars)));
        }
    }
}
