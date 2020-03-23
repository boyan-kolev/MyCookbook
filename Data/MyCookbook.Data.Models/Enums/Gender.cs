namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public enum Gender
    {
        [Description("Мъж")]
        Male = 1,

        [Description("Жена")]
        Female = 2,
    }
}
