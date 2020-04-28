namespace MyCookbook.Web.ViewModels.Categories.ByName
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Partials;

    public class CategoryByNameViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryByNameViewModel>()
                .ForMember(
                dest => dest.Recipes,
                opt => opt.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true)));
        }
    }
}
