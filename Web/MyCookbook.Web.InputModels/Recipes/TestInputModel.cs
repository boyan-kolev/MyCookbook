namespace MyCookbook.Web.InputModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Common;
    using MyCookbook.Web.ViewModels;

    public class TestInputModel
    {
        [DisplayName("Категория")]
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int[] CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
