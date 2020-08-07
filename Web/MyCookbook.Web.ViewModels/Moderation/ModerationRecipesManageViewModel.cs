namespace MyCookbook.Web.ViewModels.Moderation
{
    using System.Collections.Generic;

    public class ModerationRecipesManageViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ModerationRecipesNotApproved> Recipes { get; set; }
    }
}
