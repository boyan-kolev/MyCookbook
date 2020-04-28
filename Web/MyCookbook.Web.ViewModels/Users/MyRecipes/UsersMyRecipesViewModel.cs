namespace MyCookbook.Web.ViewModels.Users.MyRecipes
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class UsersMyRecipesViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public IEnumerable<UsersMyRecipesListViewModel> Recipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UsersMyRecipesViewModel>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true)));
        }
    }
}
