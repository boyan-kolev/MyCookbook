namespace MyCookbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.InputModels.Recipes;
    using MyCookbook.Web.ViewModels;
    using MyCookbook.Web.ViewModels.CookingMethods;
    using MyCookbook.Web.ViewModels.Cuisines;

    public class RecipesController : BaseController
    {
        private const string ImagesFolderName = "Рецепти";
        private readonly ICategoriesService categoriesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICookingMethodsService cookingMethodsService;
        private readonly ICuisinesService cuisinesService;

        public RecipesController(ICategoriesService categoriesService, ICloudinaryService cloudinaryService, ICookingMethodsService cookingMethodsService, ICuisinesService cuisinesService)
        {
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
            this.cookingMethodsService = cookingMethodsService;
            this.cuisinesService = cuisinesService;
        }


        // [RequestFormLimits(MultipartBodyLengthLimit = 700 * 1024 * 1024)]
        // [RequestSizeLimit(700 * 1024 * 1024)]
        // [HttpPost]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var cuisines = this.cuisinesService.GetAll<CuisineDropDownViewModel>();
            var cookingMethods = this.cookingMethodsService.GetAll<CookingMethodsCheckboxViewModel>();

            RecipeCreateInputModel viewModel = new RecipeCreateInputModel()
            {
                Categories = categories,
                Cuisines = cuisines,
                CookingMethods = cookingMethods,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(RecipeCreateInputModel input)
        {
            return this.Redirect("/");
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
