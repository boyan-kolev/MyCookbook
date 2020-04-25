namespace MyCookbook.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.Recipes.All;
    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Edit;
    using MyCookbook.Web.ViewModels.Recipes.Filtered;
    using MyCookbook.Web.ViewModels.Recipes.Search;

    public class RecipesController : BaseController
    {
        private const string IngredientNameError = "Името на съставката може да бъде между 2 и 80 символа!";
        private const string RecipeExistNameError = "Съществува рецепта с това име. Моля изберете друго име!";
        private const int DetailsCountOfSimilarRecipes = 6;
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly ICookingMethodsService cookingMethodsService;
        private readonly ICuisinesService cuisinesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            ICookingMethodsService cookingMethodsService,
            ICuisinesService cuisinesService,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.cookingMethodsService = cookingMethodsService;
            this.cuisinesService = cuisinesService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [Authorize]
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

            if (isExist)
            {
                this.ViewData["Errors"] += RecipeExistNameError + "\r\n";
            }

            var isAtLeastChecked = false;
            foreach (var cookingMethod in input.CookingMethods)
            {
                if (cookingMethod.Selected)
                {
                    isAtLeastChecked = true;
                    break;
                }
            }

            var isValidIngredients = true;

            foreach (var ingredientName in input.IngredientsNames.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
            {
                if (ingredientName.Length > AttributesConstraints.IngredientNameMaxLength
                    || ingredientName.Length < AttributesConstraints.IngredientNameMinLength)
                {
                    isValidIngredients = false;
                    this.ViewData["Errors"] += IngredientNameError + "\r\n";
                    break;
                }
            }

            if (!this.ModelState.IsValid || !isAtLeastChecked || isExist || !isValidIngredients)
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

            int recipeId = await this.recipesService.AddAsync(serviceModel);

            return this.RedirectToAction("Details", "Recipes", new { id = recipeId });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isUserRecipeAuthor = this.usersService.IsUserRecipeAuthor(userId, id);

            if (!isUserRecipeAuthor)
            {
                return this.BadRequest();
            }

            var viewModel = this.recipesService.GetByIdForEdit(id);
            var imagesCount = viewModel.Images?.Count ?? 0;
            this.TempData["MaxCountImages"] = AttributesConstraints.RecipeImagesMaxCount - imagesCount;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEditInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isUserRecipeAuthor = this.usersService.IsUserRecipeAuthor(userId, input.Id);
            var allowedMaxCountImages = int.Parse(this.TempData["MaxCountImages"].ToString());

            var isValidImages = true;
            if (input.Images != null)
            {
                isValidImages = input.NewImages?.Count <= allowedMaxCountImages;
                if (!isValidImages)
                {
                    this.ViewData["Errors"] += $"Можете да качите най-много {allowedMaxCountImages} снимки" + "\r\n";
                }
            }

            var isExist = this.recipesService.IsExistRecipeTitle(input.Title);
            var title = this.recipesService.GetRecipeTitle(input.Id);
            var isValidTitle = true;

            if (isExist && title.ToLower() != input.Title.ToLower())
            {
                isValidTitle = false;
                this.ViewData["Errors"] += RecipeExistNameError + "\r\n";
            }

            var isAtLeastChecked = false;
            foreach (var cookingMethod in input.AllCookingMethods)
            {
                if (cookingMethod.Selected)
                {
                    isAtLeastChecked = true;
                    break;
                }
            }

            var isValidIngredients = true;
            foreach (var ingredientName in input.IngredientsNames.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
            {
                if (ingredientName.Length > AttributesConstraints.IngredientNameMaxLength
                    || ingredientName.Length < AttributesConstraints.IngredientNameMinLength)
                {
                    isValidIngredients = false;
                    this.ViewData["Errors"] += IngredientNameError + "\r\n";
                    break;
                }
            }

            if (!this.ModelState.IsValid
                || !isAtLeastChecked
                || !isValidTitle
                || !isValidIngredients
                || !isValidImages
                || !isUserRecipeAuthor)
            {
                var categories = this.categoriesService.GetAll<RecipeEditCategoryDropDownViewModel>();
                var cuisines = this.cuisinesService.GetAll<RecipeEditCuisineDropDownViewModel>();
                var cookingMethods = this.cookingMethodsService.GetAll<RecipeEditCookingMethodsCheckboxViewModel>();

                input.Categories = categories;
                input.Cuisines = cuisines;
                input.AllCookingMethods = cookingMethods;

                var imagesCount = input.Images?.Count ?? 0;
                this.TempData["MaxCountImages"] = AttributesConstraints.RecipeImagesMaxCount - imagesCount;

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var recipeDto = new RecipeEditDto
            {
                Id = input.Id,
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
                NewImages = input.NewImages,
                IngredientsNames = input.IngredientsNames,
                CookingMethods = input.AllCookingMethods,
            };

            int recipeId = await this.recipesService.EditAsync(recipeDto);

            return this.RedirectToAction("Details", "Recipes", new { id = recipeId });
        }

        public IActionResult Details(int id)
        {
            var isExestRecipe = this.recipesService.IsExistRecipe(id);

            if (isExestRecipe == false)
            {
                return this.NotFound();
            }

            var user = this.userManager.GetUserId(this.User);
            var viewModel = this.recipesService.GetByIdForDetails(id, user, DetailsCountOfSimilarRecipes);


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

            var filteredModel = AutoMapperConfig.MapperInstance.Map<RecipeSearchInputModel, RecipeFilteredInputModel>(input);

            return this.RedirectToAction("Filtered", "Recipes", filteredModel);
        }

        public IActionResult Filtered(RecipeFilteredInputModel input)
        {
            var inputDto = AutoMapperConfig.MapperInstance.Map<RecipeFilteredInputModel, RecipeFilteredInputDto>(input);
            var viewModel = this.recipesService.GetFiltered(inputDto);

            return this.View(viewModel);
        }
    }
}
