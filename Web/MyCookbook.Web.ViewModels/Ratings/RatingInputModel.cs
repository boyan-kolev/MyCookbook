namespace MyCookbook.Web.ViewModels.Ratings
{
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Common;

    public class RatingInputModel
    {
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        [Range(
            AttributesConstraints.RatingStarsMinCount,
            AttributesConstraints.RatingStarsMaxCount,
            ErrorMessage = AttributesErrorMessages.RangeErrorMessage)]
        public int Stars { get; set; }
    }
}
