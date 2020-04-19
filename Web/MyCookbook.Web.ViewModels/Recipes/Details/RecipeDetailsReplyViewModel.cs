namespace MyCookbook.Web.ViewModels.Recipes.Details
{
    using System;

    using MyCookbook.Data.Models;
    using MyCookbook.Services.Mapping;

    public class RecipeDetailsReplyViewModel : IMapFrom<Reply>
    {
        public string Id { get; set; }

        public string CommentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public RecipeDetailsReplyUserViewModel User { get; set; }
    }
}
