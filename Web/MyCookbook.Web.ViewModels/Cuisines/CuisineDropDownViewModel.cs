namespace MyCookbook.Web.ViewModels.Cuisines
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class CuisineDropDownViewModel : IMapFrom<Cuisine>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
