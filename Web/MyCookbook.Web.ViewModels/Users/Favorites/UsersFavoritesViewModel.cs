namespace MyCookbook.Web.ViewModels.Users.Favorites
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class UsersFavoritesViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public IEnumerable<UsersFavoritesRecipesViewModel> Recipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UsersFavoritesViewModel>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(x => x.FavoriteRecipes.Select(r => r.Recipe)));
        }
    }
}
