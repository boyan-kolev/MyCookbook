namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Seasonal
    {
        [Display(Name = "Всесезонни рецепти")]
        AllSeason = 1,

        [Display(Name = "Пролетни рецепти")]
        Spring = 2,

        [Display(Name = "Летни ястия")]
        Summer = 3,

        [Display(Name = "Есенни ястия")]
        Autumn = 4,

        [Display(Name = "Зимни ястия")]
        Winter = 5,
    }
}
