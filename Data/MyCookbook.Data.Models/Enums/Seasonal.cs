namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public enum Seasonal
    {
        [Description("Всесезонни рецепти")]
        AllSeason = 1,

        [Description("Пролетни рецепти")]
        Spring = 2,

        [Description("Летни ястия")]
        Summer = 3,

        [Description("Есенни ястия")]
        Autumn = 4,

        [Description("Зимни ястия")]
        Winter = 5,
    }
}
