namespace MyCookbook.Web.ViewModels.Recipes.Details.ViewModels
{
    using System;

    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

    public class RecipeDetailsUserViewModel : IMapFrom<RecipeDetailsUserServiceModel>
    {
        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string ProfilePhoto { get; set; }
    }
}
