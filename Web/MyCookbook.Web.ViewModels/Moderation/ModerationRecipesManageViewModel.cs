namespace MyCookbook.Web.ViewModels.Moderation
{
    using System.Collections.Generic;

    public class ModerationRecipesManageViewModel
    {
        public IEnumerable<ModerationRecipesNotApproved> Recipes { get; set; }
    }
}
