namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsUserViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Gender { get; set; }

        public string ProfilePhoto { get; set; }
    }
}
