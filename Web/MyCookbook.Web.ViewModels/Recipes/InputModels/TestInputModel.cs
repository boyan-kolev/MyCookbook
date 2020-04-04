namespace MyCookbook.Web.ViewModels.Recipes.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MyCookbook.Common;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class TestInputModel
    {
        [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
        public ICollection<string> Ingredients { get; set; }
    }
}
