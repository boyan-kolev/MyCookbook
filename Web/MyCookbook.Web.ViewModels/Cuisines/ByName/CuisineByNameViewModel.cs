namespace MyCookbook.Web.ViewModels.Cuisines.ByName
{
    using System.Collections.Generic;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Partials;

    public class CuisineByNameViewModel : IMapFrom<Cuisine>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ListRecipesCollectionPartailViewModel> Recipes { get; set; }
    }
}
