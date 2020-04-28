namespace MyCookbook.Web.ViewModels.Administration.Categories.Manage
{
    using System.Collections.Generic;

    public class AdminCategoryViewModel
    {
        public IEnumerable<AdminCategoriesAllViewModel> Categories { get; set; }
    }
}
