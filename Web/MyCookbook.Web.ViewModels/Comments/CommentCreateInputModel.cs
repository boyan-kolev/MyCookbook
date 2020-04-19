namespace MyCookbook.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Common;

    public class CommentCreateInputModel
    {
        [Required]
        public int RecipeId { get; set; }

        public string CommentId { get; set; }

        [Required]
        [Display(Name = "Съдържание")]
        [StringLength(
            AttributesConstraints.CommentContentMaxLength,
            MinimumLength = AttributesConstraints.CommentContentMinLength,
            ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
        public string Content { get; set; }

    }
}
