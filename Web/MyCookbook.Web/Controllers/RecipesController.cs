namespace MyCookbook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.CookingMethods.ViewModels;
    using MyCookbook.Web.ViewModels.Recipes;
    using MyCookbook.Web.ViewModels.Recipes.InputModels;
    using MyCookbook.Web.ViewModels.Recipes.ViewModels;

    public class RecipesController : BaseController
    {
        private const string ImagesFolderName = "Рецепти";
        private readonly ICategoriesService categoriesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICookingMethodsService cookingMethodsService;

        public RecipesController(ICategoriesService categoriesService, ICloudinaryService cloudinaryService, ICookingMethodsService cookingMethodsService)
        {
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
            this.cookingMethodsService = cookingMethodsService;
        }


        // [RequestFormLimits(MultipartBodyLengthLimit = 700 * 1024 * 1024)]
        // [RequestSizeLimit(700 * 1024 * 1024)]
        // [HttpPost]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            RecipeCreateInputModel viewModel = new RecipeCreateInputModel();
            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        //[HttpPost]
        //public IActionResult Create(TestInputModel input)
        //{
        //    var isAtLeastChecked = false;
        //    foreach (var cookingMethod in input.CookingMethods)
        //    {
        //        if (cookingMethod.Selected)
        //        {
        //            isAtLeastChecked = true;
        //            break;
        //        }
        //    }

        //    if (!this.ModelState.IsValid || !isAtLeastChecked)
        //    {
        //        return this.View(input);
        //    }

        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View();
        //    }

        //    return this.Redirect("/");
        //}
    }
}
