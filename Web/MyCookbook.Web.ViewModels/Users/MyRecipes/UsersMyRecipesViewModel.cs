namespace MyCookbook.Web.ViewModels.Users.MyRecipes
{
    using System.Collections.Generic;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class UsersMyRecipesViewModel : IMapFrom<ApplicationUser>
    {
        public IEnumerable<UsersMyRecipesListViewModel> Recipes { get; set; }
    }
}
