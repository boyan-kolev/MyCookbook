namespace MyCookbook.Web.ViewModels.Categories.ByName
{
    using System.Collections.Generic;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Partials;

    public class CategoryByNameViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ListRecipesCollectionPartailViewModel> ApprovedRecipes { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Category, CategoryByNameViewModel>()
        //        .ForMember(
        //        dest => dest.Recipes,
        //        opt => opt.MapFrom(x => x.Recipes.Where(r => r.IsApproved == true)));
        //}
    }
}
