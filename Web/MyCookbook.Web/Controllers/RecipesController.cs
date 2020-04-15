namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels;
    using MyCookbook.Web.ViewModels.CookingMethods;
    using MyCookbook.Web.ViewModels.Cuisines;
    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Details.ViewModels;

    public class RecipesController : BaseController
    {
        private const string ImagesFolderName = "Рецепти";
        private const int DetailsCountOfSimilarRecipes = 20;
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
                var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
                var cuisines = this.cuisinesService.GetAll<CuisineDropDownViewModel>();
                var cookingMethods = this.cookingMethodsService.GetAll<CookingMethodsCheckboxViewModel>();

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
            var userId = this.userManager.GetUserId(this.User);
            var serviceModel = this.recipesService.GetById(id, userId, DetailsCountOfSimilarRecipes);
            var viewModel = AutoMapperConfig.MapperInstance.Map<RecipeDetailsViewModel>(serviceModel);

            return this.View(viewModel);
        }
    }
}
