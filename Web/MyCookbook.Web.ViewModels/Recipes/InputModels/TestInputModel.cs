namespace MyCookbook.Web.ViewModels.Recipes.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Common;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;
    using MyCookbook.Web.ViewModels.CookingMethods.ViewModels;
    using MyCookbook.Web.ViewModels.Recipes.ViewModels;

    public class TestInputModel
    {
        [DisplayName("Категория")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int[] CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
