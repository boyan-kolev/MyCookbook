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
    using MyCookbook.Web.ViewModels.Recipes.Details.ServiceModels;

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

        public RecipeDetailsServiceModel GetById(int recipeId, string userId, int countOfSimilarRecipes)
        {
            var serviceModel = this.recipesRepository
                .All()
                .Where(x => x.Id == recipeId)
                .To<RecipeDetailsServiceModel>()
                .FirstOrDefault();

            //var recipe = this.recipesRepository.All()
            //    .Where(r => r.Id == recipeId).FirstOrDefault();

            // var userStars = recipe.Ratings.FirstOrDefault(r => r.UserId == userId).Stars;
            // var userFavorite = recipe.FavoritedBy.Any(f => f.UserId == userId);

            if (userId != null)
            {
                var userInfo = this.recipesRepository
                    .All()
                    .Where(r => r.Id == recipeId)
                    .Select(x => new RecipeDetailsUserServiceModel
                    {
                        UsersStars = x.Ratings.FirstOrDefault(r => r.UserId == userId).Stars,
                        IsUserFavorite = x.FavoritedBy.Any(f => f.UserId == userId),
                        IsUserCooked = x.CookedBy.Any(c => c.UserId == userId),
                    })
                    .FirstOrDefault();

                serviceModel.User = userInfo;

                //serviceModel.User = new RecipeDetailsUserServiceModel
                //{
                //    UsersStars = userId.Ratings.Where(r => r.RecipeId == recipeId).Select(r => r.Stars).FirstOrDefault(),
                //    IsUserFavorite = userId.FavoriteRecipes.Any(r => r.RecipeId == recipeId),
                //    IsUserCooked = userId.CookedRecipes.Any(r => r.RecipeId == recipeId),
                //};

                //serviceModel.User = userInfo;
            }


            //var userStars = this.recipesRepository
            //    .All()
            //    .Where(x => x.Id == recipeId)
            //    .Select(x => x.Ratings.FirstOrDefault(f => f.UserId == userId).Stars)
            //    .FirstOrDefault();


            // var userStars = recipe.Ratings.Where(x => x.UserId == userId).Select(r => r.Stars).FirstOrDefault();
            var authorAge = this.usersService.GetAge(serviceModel.Author.Birthdate);
            var similarRecipes = this.GetAllFromCategory<RecipeDetailsSimilarRecipesServiceModel>(
                serviceModel.CategoryId,
                countOfSimilarRecipes,
                recipeId);

            serviceModel.Author.Age = authorAge;
            serviceModel.SimilarRecipes = similarRecipes;
            //serviceModel.UsersStars = userStars;

            if (serviceModel.Images.Length < 1)
            {
                serviceModel.Images = new RecipeDetailsImagesServiceModel[1];
                RecipeDetailsImagesServiceModel recipeDefaultImage = new RecipeDetailsImagesServiceModel
                {
                    Url = GlobalConstants.DefaultRecipeImageUrl,
                    IsTitlePhoto = true,
                };

                serviceModel.Images[0] = recipeDefaultImage;
            }

            if (serviceModel.Author.ProfilePhoto == null)
            {
                if (serviceModel.Author.Gender == Gender.Male.ToString())
                {
                    serviceModel.Author.ProfilePhoto = GlobalConstants.DefaultUserPhotoMaleUrl;
                }
                else
                {
                    serviceModel.Author.ProfilePhoto = GlobalConstants.DefaultUserPhotoFemaleUrl;
                }
            }

            return serviceModel;
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

        public int GetCookTimes(int recipeId)
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
