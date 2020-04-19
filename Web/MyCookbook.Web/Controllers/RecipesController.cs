namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Recipes.All;
    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Search;

    public class RecipesController : BaseController
    {
        private const string ImagesFolderName = "Рецепти";
        private const int DetailsCountOfSimilarRecipes = 9;
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICookingMethodsService cookingMethodsService;
        private readonly ICuisinesService cuisinesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            ICloudinaryService cloudinaryService,
            ICookingMethodsService cookingMethodsService,
            ICuisinesService cuisinesService,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
            this.cookingMethodsService = cookingMethodsService;
            this.cuisinesService = cuisinesService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<RecipeCreateCategoryDropDownViewModel>();
            var cuisines = this.cuisinesService.GetAll<RecipeCreateCuisineDropDownViewModel>();
            var cookingMethods = this.cookingMethodsService.GetAll<RecipeCreateCookingMethodsCheckboxViewModel>();

            RecipeCreateInputModel viewModel = new RecipeCreateInputModel()
            {
                Categories = categories,
                Cuisines = cuisines,
                CookingMethods = cookingMethods,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [RequestSizeLimit(700 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 700 * 1024 * 1024)]
        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateInputModel input)
        {
            var isExist = this.recipesService.IsExistRecipeTitle(input.Title);

            var isAtLeastChecked = false;
            foreach (var cookingMethod in input.CookingMethods)
            {
                if (cookingMethod.Selected)
                {
                    isAtLeastChecked = true;
                    break;
                }
            }

            if (!this.ModelState.IsValid || !isAtLeastChecked || isExist)
            {
                var categories = this.categoriesService.GetAll<RecipeCreateCategoryDropDownViewModel>();
                var cuisines = this.cuisinesService.GetAll<RecipeCreateCuisineDropDownViewModel>();
                var cookingMethods = this.cookingMethodsService.GetAll<RecipeCreateCookingMethodsCheckboxViewModel>();

                input.Categories = categories;
                input.Cuisines = cuisines;
                input.CookingMethods = cookingMethods;

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var serviceModel = new RecipeCreateServiceModel
            {
                Title = input.Title,
                AuthorId = user.Id,
                Description = input.Description,
                Advices = input.Advices,
                Servings = input.Servings,
                PrepTime = input.PrepTime,
                CookTime = input.CookTime,
                SeasonalType = input.SeasonalType,
                SkillLevel = input.SkillLevel,
                CategoryId = input.CategoryId,
                CuisineId = input.CuisineId,
                Images = input.Images,
                TitleImage = input.TitleImage,
                IngredientsNames = input.IngredientsNames,
                CookingMethods = input.CookingMethods,
            };

            await this.recipesService.AddAsync(serviceModel);

            return this.Redirect("/");
        }

        public IActionResult Details(int id)
        {
            var user = this.userManager.GetUserId(this.User);
            var viewModel = this.recipesService.GetById(id, user, DetailsCountOfSimilarRecipes);

            return this.View(viewModel);
        }

        public IActionResult All()
        {
            var recipes = this.recipesService.GetAll<RecipeAllRecipesViewModel>();
            var viewModel = new RecipeAllViewModel { Recipes = recipes };

            return this.View(viewModel);
        }

        public IActionResult Search()
        {
            var categories = this.categoriesService.GetAll<RecipeSearchCategoryDropDownViewModel>();
            var cuisines = this.cuisinesService.GetAll<RecipeSearchCuisineDropDownViewModel>();
            var cookingMethods = this.cookingMethodsService.GetAll<RecipeSearchCookingMethodsDropDownViewModel>();

            var viewModel = new RecipeSearchInputModel()
            {
                Categories = categories,
                Cuisines = cuisines,
                CookingMethods = cookingMethods,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(RecipeSearchInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.categoriesService.GetAll<RecipeSearchCategoryDropDownViewModel>();
                var cuisines = this.cuisinesService.GetAll<RecipeSearchCuisineDropDownViewModel>();
                var cookingMethods = this.cookingMethodsService.GetAll<RecipeSearchCookingMethodsDropDownViewModel>();

                var viewModel = new RecipeSearchInputModel()
                {
                    Categories = categories,
                    Cuisines = cuisines,
                    CookingMethods = cookingMethods,
                };

                return this.View(viewModel);
            }

            return this.Redirect("/");
        }
    }
}
