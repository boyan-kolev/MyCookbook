namespace MyCookbook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyCookbook.Common;
    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Data.Models.Enums;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Web.ViewModels.CookingMethods;
    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Details.ViewModels;

    public class RecipesService : IRecipesService
    {
        private const string CloudinaryFolderName = "Рецепти";

        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<CookingMethod> cookingMethodsRepository;
        private readonly IIngredientsService ingredientsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUsersService usersService;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<CookingMethod> cookingMethodRepository,
            IIngredientsService ingredientsService,
            ICloudinaryService cloudinaryService,
            IUsersService usersService)
        {
            this.recipesRepository = recipesRepository;
            this.cookingMethodsRepository = cookingMethodRepository;
            this.ingredientsService = ingredientsService;
            this.cloudinaryService = cloudinaryService;
            this.usersService = usersService;
        }

        public async Task AddAsync(RecipeCreateServiceModel model)
        {
            var recipe = new Recipe()
            {
                Title = model.Title,
                Description = model.Description,
                Advices = model.Advices,
                Servings = model.Servings,
                PrepTime = model.PrepTime,
                CookTime = model.CookTime,
                SeasonalType = model.SeasonalType,
                SkillLevel = model.SkillLevel,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                CuisineId = model.CuisineId,
            };

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();

            if (model.Images != null)
            {
                foreach (var imageFile in model.Images)
                {
                    var imageUrl = await this.cloudinaryService.UploadAsync(imageFile, imageFile.FileName, CloudinaryFolderName);

                    var image = new Image()
                    {
                        Url = imageUrl,
                        RecipeId = recipe.Id,
                    };

                    recipe.Images.Add(image);
                }
            }

            if (model.TitleImage != null)
            {
                var titleImageUrl = await this.cloudinaryService.UploadAsync(model.TitleImage, model.TitleImage.FileName, CloudinaryFolderName);
                var titleImage = new Image()
                {
                    Url = titleImageUrl,
                    IsTitlePhoto = true,
                    RecipeId = recipe.Id,
                };
                recipe.Images.Add(titleImage);
            }

            var ingredientsNames = model.IngredientsNames.Split("\r\n");

            foreach (var ingredientName in ingredientsNames)
            {
                await this.ingredientsService.SetIngredientToRecipeAsync(ingredientName, recipe.Id);
            }

            await this.SetRecipeToRecipeCookingMthodsAsync(model.CookingMethods, recipe.Id);
            await this.recipesRepository.SaveChangesAsync();
        }

        public RecipeDetailsViewModel GetById(int recipeId, string userId, int countOfSimilarRecipes)
        {
            var viewModel = this.recipesRepository
                .All()
                .Where(x => x.Id == recipeId)
                .To<RecipeDetailsViewModel>()
                .FirstOrDefault();

            if (userId != null)
            {
                var userInfo = this.recipesRepository
                    .All()
                    .Where(r => r.Id == recipeId)
                    .Select(x => new RecipeDetailsUserViewModel
                    {
                        UsersStars = x.Ratings.FirstOrDefault(r => r.UserId == userId).Stars,
                        IsUserFavorite = x.FavoritedBy.Any(f => f.UserId == userId),
                        IsUserCooked = x.CookedBy.Any(c => c.UserId == userId),
                    })
                    .FirstOrDefault();

                viewModel.User = userInfo;
            }

            var authorAge = this.usersService.GetAge(viewModel.Author.Birthdate);
            var similarRecipes = this.GetAllFromCategory<RecipeDetailsSimilarRecipesViewModel>(
                viewModel.CategoryId,
                countOfSimilarRecipes,
                recipeId);

            viewModel.Author.Age = authorAge;
            viewModel.SimilarRecipes = similarRecipes;

            if (viewModel.Images.Length < 1)
            {
                viewModel.Images = new RecipeDetailsImagesViewModel[1];
                RecipeDetailsImagesViewModel recipeDefaultImage = new RecipeDetailsImagesViewModel
                {
                    Url = GlobalConstants.DefaultRecipeImageUrl,
                    IsTitlePhoto = true,
                };

                viewModel.Images[0] = recipeDefaultImage;
            }

            if (viewModel.Author.ProfilePhoto == null)
            {
                if (viewModel.Author.Gender == Gender.Male.ToString())
                {
                    viewModel.Author.ProfilePhoto = GlobalConstants.DefaultUserPhotoMaleUrl;
                }
                else
                {
                    viewModel.Author.ProfilePhoto = GlobalConstants.DefaultUserPhotoFemaleUrl;
                }
            }

            return viewModel;
        }

        public IEnumerable<T> GetAllFromCategory<T>(
            int categoryId,
            int? count = null,
            int? withoutRecipeId = null)
        {
            IQueryable<Recipe> query = this.recipesRepository
                .All()
                .Where(r => r.CategoryId == categoryId)
                .OrderBy(r => r.CreatedOn);

            if (withoutRecipeId.HasValue)
            {
                query = query.Where(r => r.Id != withoutRecipeId);
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public bool IsExistRecipeTitle(string recipeTtile)
        {
            return this.recipesRepository.All().Any(x => x.Title == recipeTtile);
        }

        public async Task<bool> SetRecipeToUserFavoriteRecipesAsync(int recipeId, string userId)
        {
            var recipe = await this.recipesRepository.GetByIdWithDeletedAsync(recipeId);
            var userFavoriteRecipe = this.recipesRepository
                .All()
                .Where(x => x.Id == recipeId)
                .Select(x => x.FavoritedBy
                .FirstOrDefault(r => r.UserId == userId))
                .FirstOrDefault();

            var result = false;

            if (userFavoriteRecipe != null)
            {
                recipe.FavoritedBy.Remove(userFavoriteRecipe);
                result = false;
            }
            else
            {
                recipe.FavoritedBy.Add(new UserFavoriteRecipe { UserId = userId });
                result = true;
            }

            await this.recipesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> SetRecipeToUserCookedRecipesAsync(int recipeId, string userId)
        {
            var recipe = await this.recipesRepository.GetByIdWithDeletedAsync(recipeId);
            var userCookedRecipe = this.recipesRepository
                .All()
                .Where(x => x.Id == recipeId)
                .Select(x => x.CookedBy
                .FirstOrDefault(r => r.UserId == userId))
                .FirstOrDefault();

            var result = false;

            if (userCookedRecipe != null)
            {
                recipe.CookedBy.Remove(userCookedRecipe);
                result = false;
            }
            else
            {
                recipe.CookedBy.Add(new UserCookedRecipe { UserId = userId });
                result = true;
            }

            await this.recipesRepository.SaveChangesAsync();

            return result;
        }

        public int GetCookTimesById(int recipeId)
        {
            var cookTimes = this.recipesRepository
                .All()
                .Where(r => r.Id == recipeId)
                .Select(r => r.CookedBy.Count)
                .FirstOrDefault();

            return cookTimes;
        }

        private async Task SetRecipeToRecipeCookingMthodsAsync(
            CookingMethodsCheckboxViewModel[] cookingMethodsCheckBox,
            int recipeId)
        {
            var recipe = await this.recipesRepository.GetByIdWithDeletedAsync(recipeId);

            foreach (var cookingMethodModel in cookingMethodsCheckBox)
            {
                // TODO add validation if null
                if (cookingMethodModel.Selected)
                {
                    var cookingMethod = await this.cookingMethodsRepository
                        .GetByIdWithDeletedAsync(cookingMethodModel.Id);

                    recipe.RecipesCookingMethods.Add(new RecipeCookingMethod
                    {
                        CookingMethod = cookingMethod,
                    });
                }
            }

            this.recipesRepository.Update(recipe);
        }
    }
}
