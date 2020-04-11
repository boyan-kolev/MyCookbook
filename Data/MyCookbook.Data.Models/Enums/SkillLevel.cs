namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum SkillLevel
    {
        [Display(Name = "Начинаещи")]
        Beginner = 1,

        [Display(Name = "Напреднали")]
        Advanced = 2,

        [Display(Name = "Професионалисти")]
        Professional = 3,
    }
}
