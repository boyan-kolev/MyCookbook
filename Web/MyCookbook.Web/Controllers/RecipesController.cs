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
    using MyCookbook.Web.ViewModels.Recipes;
    using MyCookbook.Web.ViewModels.Recipes.InputModels;
    using MyCookbook.Web.ViewModels.Recipes.ViewModels;

    public class RecipesController : BaseController
    {
        private const string ImagesFolderName = "Рецепти";
        private readonly ICategoriesService categoriesService;
        private readonly ICloudinaryService cloudinaryService;

        public RecipesController(ICategoriesService categoriesService, ICloudinaryService cloudinaryService)
        {
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            //var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            //var viewModel = new RecipeCreateInputModel
            //{
            //    Categories = categories,
            //};

            //return this.View(viewModel);

            return this.View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 700 * 1024 * 1024)]
        [RequestSizeLimit(700 * 1024 * 1024)]
        public async Task<IActionResult> Create(TestInputModel input)
        {
            // if (this.ModelState.IsValid == false)
            // {
            //    return this.View(files);
            // }

            if (this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.Redirect("/");
        }
    }
}
