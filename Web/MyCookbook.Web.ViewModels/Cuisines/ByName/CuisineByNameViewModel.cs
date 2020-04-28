namespace MyCookbook.Web.ViewModels.Cuisines.ByName
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Partials;

    public class CuisineByNameViewModel : IMapFrom<Cuisine>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Cuisine, CuisineByNameViewModel>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true)));
        }
    }
}
