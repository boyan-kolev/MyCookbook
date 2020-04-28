namespace MyCookbook.Web.ViewModels.Users.Cooked
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Partials;

    public class UsersCookedViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UsersCookedViewModel>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(x => x.CookedRecipes.Select(r => r.Recipe).Where(r => r.IsApproved == true)));
        }
    }
}
