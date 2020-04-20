namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public int UsersStars { get; set; }

        public bool IsUserFavorite { get; set; }

        public bool IsUserCooked { get; set; }
    }
}
