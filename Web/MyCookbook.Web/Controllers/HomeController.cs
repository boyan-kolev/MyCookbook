namespace MyCookbook.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels;
    using MyCookbook.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int CountOfCategories = 6;
        private const int CountOfCuisines = 9;
        private const int CountOfTopRecipes = 6;
        private readonly ICuisinesService cuisinesService;
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public HomeController(
            ICuisinesService cuisinesService,
            ICategoriesService categoriesService,
            IRecipesService recipesService)
        {
            this.cuisinesService = cuisinesService;
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
        }

        public IActionResult Index()
        {
            var categories = this.categoriesService.GetAll<IndexCategoriesViewModel>(CountOfCategories);
            var cuisines = this.cuisinesService.GetAll<IndexCuisinesViewModel>(CountOfCuisines);
            var topRecipes = this.recipesService.GetTopRecipes<IndexTopRecipesViewModel>(CountOfTopRecipes);

            var viewModel = new IndexViewModel
            {
                Categories = categories,
                Cuisines = cuisines,
                TopRecipes = topRecipes,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
