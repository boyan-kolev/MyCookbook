namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum SkillLevel
    {
        [Display(Name = "Начинаещ")]
        Beginner = 1,

        [Display(Name = "Напреднал")]
        Advanced = 2,

        [Display(Name = "Професионалист")]
        Professional = 3,
    }
}
