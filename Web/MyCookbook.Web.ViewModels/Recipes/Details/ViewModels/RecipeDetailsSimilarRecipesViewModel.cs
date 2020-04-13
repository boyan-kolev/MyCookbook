namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using System;

    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsSimilarRecipesViewModel : IMapFrom<RecipeDetailsSimilarRecipesServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitlePhotoUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
