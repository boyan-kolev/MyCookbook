namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Gender
    {
        [Display(Name = "Мъж")]
        Male = 1,

        [Display(Name = "Жена")]
        Female = 2,
    }
}
