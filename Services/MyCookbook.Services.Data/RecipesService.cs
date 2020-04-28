namespace MyCookbook.Services.Data
{
    using System;
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
    using MyCookbook.Web.ViewModels.Partials;
    using MyCookbook.Web.ViewModels.Recipes;
    using MyCookbook.Web.ViewModels.Recipes.Create;
    using MyCookbook.Web.ViewModels.Recipes.Details;
    using MyCookbook.Web.ViewModels.Recipes.Edit;
    using MyCookbook.Web.ViewModels.Recipes.Filtered;
    using MyCookbook.Web.ViewModels.Recipes.Search;

    public class RecipesService : IRecipesService
    {
        private const string CloudinaryFolderName = "Рецепти";

        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<CookingMethod> cookingMethodsRepository;
        private readonly IIngredientsService ingredientsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUsersService usersService;
        private readonly ICategoriesService categoriesService;
        private readonly ICuisinesService cuisinesService;
        private readonly ICookingMethodsService cookingMethodsService;
        private readonly IImagesService imagesService;
        private readonly ICommentsService commentsService;
        private readonly IRatingsService ratingsService;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<CookingMethod> cookingMethodRepository,
            IIngredientsService ingredientsService,
            ICloudinaryService cloudinaryService,
            IUsersService usersService,
            ICategoriesService categoriesService,
            ICuisinesService cuisinesService,
            ICookingMethodsService cookingMethodsService,
            IImagesService imagesService,
            ICommentsService commentsService,
            IRatingsService ratingsService)
        {
            this.recipesRepository = recipesRepository;
            this.cookingMethodsRepository = cookingMethodRepository;
            this.ingredientsService = ingredientsService;
            this.cloudinaryService = cloudinaryService;
            this.usersService = usersService;
            this.categoriesService = categoriesService;
            this.cuisinesService = cuisinesService;
            this.cookingMethodsService = cookingMethodsService;
            this.imagesService = imagesService;
            this.commentsService = commentsService;
            this.ratingsService = ratingsService;
        }

        public async Task<int> AddAsync(RecipeCreateServiceModel model)
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
                IsApproved = false,
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

                    await this.imagesService.AddAsync(image);
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

                await this.imagesService.AddAsync(titleImage);
            }

            var ingredientsNames = model.IngredientsNames.Split("\r\n");

            foreach (var ingredientName in ingredientsNames)
            {
                await this.ingredientsService.SetIngredientToRecipeAsync(ingredientName, recipe.Id);
            }

            foreach (var cookingMethod in model.CookingMethods)
            {
                if (cookingMethod.Selected)
                {
                    await this.SetRecipeToRecipeCookingMethodsAsync(cookingMethod.Id, recipe.Id);
                }
            }

            await this.recipesRepository.SaveChangesAsync();

            return recipe.Id;
        }

        public async Task<int> EditAsync(RecipeEditDto model)
        {
            var recipe = this.recipesRepository
                .All()
                .FirstOrDefault(r => r.Id == model.Id);

            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Advices = model.Advices;
            recipe.Servings = model.Servings;
            recipe.PrepTime = model.PrepTime;
            recipe.CookTime = model.CookTime;
            recipe.SeasonalType = model.SeasonalType;
            recipe.SkillLevel = model.SkillLevel;
            recipe.AuthorId = model.AuthorId;
            recipe.CategoryId = model.CategoryId;
            recipe.CuisineId = model.CuisineId;
            recipe.IsApproved = false;

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();

            if (model.NewImages != null)
            {
                foreach (var imageFile in model.NewImages)
                {
                    var imageUrl = await this.cloudinaryService.UploadAsync(imageFile, imageFile.FileName, CloudinaryFolderName);

                    var image = new Image()
                    {
                        Url = imageUrl,
                        RecipeId = recipe.Id,
                    };

                    await this.imagesService.AddAsync(image);
                }
            }

            await this.ingredientsService.DeleteIngredientsByRecipeIdAsync(recipe.Id);

            var ingredientsNames = model.IngredientsNames.Split("\r\n");
            foreach (var ingredientName in ingredientsNames)
            {
                await this.ingredientsService.SetIngredientToRecipeAsync(ingredientName, recipe.Id);
            }

            await this.DeleteCookingMethodInRecipe(recipe.Id);

            foreach (var cookingMethod in model.CookingMethods)
            {
                if (cookingMethod.Selected)
                {
                    await this.SetRecipeToRecipeCookingMethodsAsync(cookingMethod.Id, recipe.Id);
                }
            }

            await this.recipesRepository.SaveChangesAsync();

            return recipe.Id;
        }

        public async Task DeleteAsync(int recipeId)
        {
            await this.commentsService.DeleteCommentsByRecipeIdAsync(recipeId);
            await this.DeleteCookingMethodInRecipe(recipeId);
            await this.imagesService.DeleteImagesByRecipeIdAsync(recipeId);
            await this.ingredientsService.DeleteIngredientsByRecipeIdAsync(recipeId);
            await this.ratingsService.DeleteRatingsByRecipeIdAsync(recipeId);

            var recipe = this.recipesRepository
                .All()
                .FirstOrDefault(r => r.Id == recipeId);

            var favoritedRecipes = this.recipesRepository
                .All()
                .Where(r => r.Id == recipeId)
                .Select(r => r.FavoritedBy)
                .FirstOrDefault();

            var cookedRecipes = this.recipesRepository
                .All()
                .Where(r => r.Id == recipeId)
                .Select(r => r.CookedBy)
                .FirstOrDefault();

            recipe.FavoritedBy.Clear();
            recipe.CookedBy.Clear();

            this.recipesRepository.Delete(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public RecipeEditInputModel GetByIdForEdit(int recipeId)
        {
            var recipe = this.recipesRepository
                .All()
                .Where(r => r.Id == recipeId && r.IsApproved == true)
                .To<RecipeEditInputModel>()
                .FirstOrDefault();

            recipe.Categories = this.categoriesService.GetAll<RecipeEditCategoryDropDownViewModel>();
            recipe.Cuisines = this.cuisinesService.GetAll<RecipeEditCuisineDropDownViewModel>();
            recipe.AllCookingMethods = this.cookingMethodsService.GetAll<RecipeEditCookingMethodsCheckboxViewModel>();

            foreach (var cookingMethodId in recipe.RecipeCookingMethodsIds)
            {
                foreach (var cookingMethod in recipe.AllCookingMethods)
                {
                    if (cookingMethodId == cookingMethod.Id)
                    {
                        cookingMethod.Selected = true;
                    }
                }
            }

            return recipe;
        }

        public RecipeDetailsViewModel GetByIdForDetails(int recipeId, string userId, int countOfSimilarRecipes, bool isWithNotApproved)
        {
            var query = this.recipesRepository
                .All()
                .Where(x => x.Id == recipeId);

            if (isWithNotApproved == false)
            {
                query = query.Where(x => x.IsApproved == true);
            }

            var viewModel = query.To<RecipeDetailsViewModel>().FirstOrDefault();

            if (viewModel == null)
            {
                return null;
            }

            if (userId != null)
            {
                var userInfo = this.recipesRepository
                    .All()
                    .Where(r => r.Id == recipeId)
                    .Select(x => new RecipeDetailsUserViewModel
                    {
                        Id = userId,
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

            foreach (var comment in viewModel.Comments)
            {
                comment.User.Age = this.usersService.GetAge(comment.User.Birthdate);

                if (comment.User.ProfilePhoto == null)
                {
                    if (comment.User.Gender == Gender.Male)
                    {
                        comment.User.ProfilePhoto = GlobalConstants.DefaultUserPhotoMaleUrl;
                    }
                    else
                    {
                        comment.User.ProfilePhoto = GlobalConstants.DefaultUserPhotoFemaleUrl;
                    }
                }

                foreach (var reply in comment.Replies)
                {
                    if (reply.User.ProfilePhoto == null)
                    {
                        if (reply.User.Gender == Gender.Male)
                        {
                            reply.User.ProfilePhoto = GlobalConstants.DefaultUserPhotoMaleUrl;
                        }
                        else
                        {
                            reply.User.ProfilePhoto = GlobalConstants.DefaultUserPhotoFemaleUrl;
                        }
                    }

                    reply.User.Age = this.usersService.GetAge(reply.User.Birthdate);
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
                .Where(r => r.CategoryId == categoryId && r.IsApproved == true)
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
            return this.recipesRepository.All().Any(x => x.Title.ToLower() == recipeTtile.ToLower());
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

        public IEnumerable<T> GetAll<T>(bool isWithApproved)
        {
            var recipes = this.recipesRepository
                .All()
                .Where(r => r.IsApproved == isWithApproved)
                .OrderByDescending(r => r.CreatedOn)
                .To<T>();

            return recipes;
        }

        public RecipeFilteredViewModel GetFiltered(RecipeFilteredInputDto input)
        {
            var viewModel = new RecipeFilteredViewModel();
            var query = this.recipesRepository.All().Where(r => r.IsApproved == true);

            if (!string.IsNullOrEmpty(input.Title) && !string.IsNullOrWhiteSpace(input.Title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(input.Title.ToLower()));
            }

            if (input.CategoryId > 0)
            {
                query = query.Where(x => x.CategoryId == input.CategoryId);
                viewModel.Category = this.categoriesService.GetNameById(input.CategoryId);
            }

            if (input.CuisineId > 0)
            {
                query = query.Where(x => x.CuisineId == input.CuisineId);
                viewModel.Cuisine = this.cuisinesService.GetById<RecipeFilteredCuisineViewModel>(input.CuisineId);
            }

            if (input.CookingMethodId > 0)
            {
                query = query.Where(x => x.RecipesCookingMethods.Any(c => c.CookingMethodId == input.CookingMethodId));
                viewModel.CookingMethod = this.cookingMethodsService.GetNameById(input.CookingMethodId);
            }

            if (input.IsCheckedPrepTime)
            {
                query = query.Where(x => x.PrepTime <= input.PrepTime);
                viewModel.PrepTime = input.PrepTime;
                viewModel.IsCheckedPrepTime = true;
            }

            if (input.IsCheckedCookTime)
            {
                query = query.Where(x => x.CookTime <= input.CookTime);
                viewModel.CookTime = input.CookTime;
                viewModel.IsCheckedCookTime = true;
            }

            if (input.IsCheckedSeasonalType)
            {
                query = query.Where(x => x.SeasonalType == input.SeasonalType);
                viewModel.IsCheckedSeasonalType = true;
            }

            if (input.IsCheckedSkillLevel)
            {
                query = query.Where(x => x.SkillLevel == input.SkillLevel);
                viewModel.IsCheckedSkillLevel = true;
            }

            switch (input.SortedType)
            {
                case SortedType.DateAscending:
                    query = query.OrderByDescending(x => x.CreatedOn);
                    break;
                case SortedType.DateDescending:
                    query = query.OrderBy(x => x.CreatedOn);
                    break;
                case SortedType.RatingAscending:
                    query = query
                        .OrderByDescending(x => x.Ratings
                        .Average(r => r.Stars))
                        .ThenByDescending(r => r.Ratings.Count);
                    break;
                case SortedType.RatingDescending:
                    query = query
                        .OrderBy(x => x.Ratings
                        .Average(r => r.Stars))
                        .ThenBy(r => r.Ratings.Count());
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedOn);
                    break;
            }

            viewModel.SortedType = input.SortedType;
            viewModel.SeasonalType = input.SeasonalType;
            viewModel.SkillLevel = input.SkillLevel;

            var filteredRecipes = query.To<ListRecipesCollectionPartailViewModel>().ToList();
            viewModel.FilteredRecipes = filteredRecipes;

            return viewModel;
        }

        public IEnumerable<T> GetLastCreatedRecipes<T>(int count)
        {
            var lastCreatedRecipes = this.recipesRepository
                .All()
                .Where(r => r.IsApproved == true)
                .OrderByDescending(r => r.CreatedOn)
                .Take(count)
                .To<T>()
                .ToList();

            return lastCreatedRecipes;
        }

        public IEnumerable<T> GetTopRecipes<T>(int count)
        {
            var topRecipes = this.recipesRepository
                .All()
                .Where(r => r.IsApproved == true)
                .OrderByDescending(r => r.Ratings.Average(x => x.Stars))
                .ThenByDescending(x => x.Ratings.Count())
                .Take(count)
                .To<T>()
                .ToList();

            return topRecipes;
        }

        public bool IsExistRecipe(int id, bool isWithNotApproved)
        {
            bool isExist;

            if (isWithNotApproved)
            {
                isExist = this.recipesRepository.All().Any(r => r.Id == id);
            }
            else
            {
                isExist = this.recipesRepository.All().Any(r => r.Id == id && r.IsApproved == true);
            }

            return isExist;
        }

        public string GetRecipeTitle(int recipeId)
        {
            var title = this.recipesRepository
                .All()
                .Where(r => r.Id == recipeId)
                .Select(r => r.Title)
                .FirstOrDefault();

            return title;
        }

        public async Task Approve(int recipeId)
        {
            var recipe = this.recipesRepository
                .All()
                .Where(r => r.Id == recipeId)
                .FirstOrDefault();

            recipe.IsApproved = true;

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        private async Task SetRecipeToRecipeCookingMethodsAsync(
            int cookingMethodId,
            int recipeId)
        {
            var recipe = await this.recipesRepository.GetByIdWithDeletedAsync(recipeId);

            recipe.RecipesCookingMethods.Add(new RecipeCookingMethod
            {
                CookingMethodId = cookingMethodId,
                RecipeId = recipeId,
            });

            this.recipesRepository.Update(recipe);
        }

        private async Task DeleteCookingMethodInRecipe(int recipeId)
        {
            var recipe = await this.recipesRepository.GetByIdWithDeletedAsync(recipeId);

            var userFavoriteRecipe = this.recipesRepository
                .All()
                .Where(x => x.Id == recipeId)
                .Select(x => x.RecipesCookingMethods)
                .ToList();

            recipe.RecipesCookingMethods.Clear();
            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

    }
}
