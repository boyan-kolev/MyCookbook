namespace MyCookbook.Web.ViewModels.Administration.Categories.All
{
    using System;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class AdminCategoriesAllViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
