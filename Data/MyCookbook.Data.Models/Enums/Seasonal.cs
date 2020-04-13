namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Seasonal
    {
        [Display(Name = "Всички сезони")]
        AllSeason = 1,

        [Display(Name = "Пролетния сезон")]
        Spring = 2,

        [Display(Name = "Летния сезон")]
        Summer = 3,

        [Display(Name = "Есения сезон")]
        Autumn = 4,

        [Display(Name = "Зимния сезон")]
        Winter = 5,
    }
}
