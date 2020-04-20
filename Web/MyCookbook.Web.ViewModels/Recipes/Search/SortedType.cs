namespace MyCookbook.Web.ViewModels.Recipes.Search
{
    using System.ComponentModel.DataAnnotations;

    public enum SortedType
    {
        [Display(Name = "Дата(възходящ ред)")]
        DateAscending = 1,

        [Display(Name = "Дата(низходящ ред)")]
        DateDescending = 2,

        [Display(Name = "Рейтинг(възходящ ред)")]
        RatingAscending = 3,

        [Display(Name = "Рейтинг(низходящ ред)")]
        RatingDescending = 4,
    }
}
