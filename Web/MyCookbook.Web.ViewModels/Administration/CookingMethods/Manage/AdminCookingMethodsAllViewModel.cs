namespace MyCookbook.Web.ViewModels.Administration.CookingMethods.Manage
{
    using System;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class AdminCookingMethodsAllViewModel : IMapFrom<CookingMethod>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
