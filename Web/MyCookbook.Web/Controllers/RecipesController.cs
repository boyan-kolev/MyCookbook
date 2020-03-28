namespace MyCookbook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Recipes;
    using MyCookbook.Web.ViewModels.Recipes.InputModels;
    using MyCookbook.Web.ViewModels.Recipes.ViewModels;

    public class RecipesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public RecipesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new RecipeCreateInputModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(RecipeCreateInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            return this.Redirect("/");
        }
    }
}
