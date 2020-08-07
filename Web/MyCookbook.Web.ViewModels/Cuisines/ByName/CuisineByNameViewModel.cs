namespace MyCookbook.Web.ViewModels.Cuisines.ByName
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Partials;

    public class CuisineByNameViewModel : IMapFrom<Cuisine>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ListRecipesCollectionPartailViewModel> ApprovedRecipes { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Cuisine, CuisineByNameViewModel>()
        //        .ForMember(
        //        dest => dest.Recipes,
        //        opt => opt.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true)));
        //}
    }
}
