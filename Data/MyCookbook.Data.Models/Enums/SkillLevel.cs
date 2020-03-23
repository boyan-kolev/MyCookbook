namespace MyCookbook.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public enum SkillLevel
    {
        [Description("Начинаещ")]
        Beginner = 1,

        [Description("Напреднал")]
        Advanced = 2,

        [Description("Професионалист")]
        Professional = 3,
    }
}
