namespace MyCookbook.Web.ViewModels.Administration.CookingMethods.Manage
{
    using System.Collections.Generic;

    public class AdminCookingMethodsManageViewModel
    {
        public IEnumerable<AdminCookingMethodsAllViewModel> CookingMethods { get; set; }
    }
}
