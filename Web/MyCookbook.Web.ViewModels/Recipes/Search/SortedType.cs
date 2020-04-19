namespace MyCookbook.Web.ViewModels.Recipes.Search
{
    using System.ComponentModel.DataAnnotations;

    public enum SortedType
    {
        [Display(Name = "Дата(възходящо)")]
        DateAscending = 1,

        [Display(Name = "Дата(низходящо)")]
        DateDescending = 2,

        [Display(Name = "Рейтинг(възходящо)")]
        RatingAscending = 3,

        [Display(Name = "Рейтинг(низходящо)")]
        RatingDescending = 4,
    }
}
