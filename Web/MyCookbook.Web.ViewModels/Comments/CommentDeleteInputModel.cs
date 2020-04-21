namespace MyCookbook.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentDeleteInputModel
    {
        [Required]
        public int RecipeId { get; set; }

        [Required]
        public string CommentId { get; set; }

        public string ReplyId { get; set; }
    }
}
