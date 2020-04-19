namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;
    using System.Collections.Generic;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsCommentsViewModel : IMapFrom<Comment>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public RecipeDetailsCommentUserViewModel User { get; set; }

        public IEnumerable<RecipeDetailsReplyViewModel> Replies { get; set; }
    }
}
