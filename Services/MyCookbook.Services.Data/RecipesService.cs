namespace MyCookbook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MyCookbook.Data.Common.Repositories;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Contracts;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Services.Mapping;
    using MyCookbook.Services.Models.Recipes.Create;
    using MyCookbook.Web.ViewModels.CookingMethods;

    public class RecipesService : IRecipesService
    {
        private const string CloudinaryFolderName = "Рецепти";

        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<CookingMethod> cookingMethodsRepository;
        private readonly IIngredientsService ingredientsService;
        private readonly ICloudinaryService cloudinaryService;

        public RecipesService(IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<CookingMethod> cookingMethodRepository, IIngredientsService ingredientsService, ICloudinaryService cloudinaryService)
        {
            this.recipesRepository = recipesRepository;
            this.cookingMethodsRepository = cookingMethodRepository;
            this.ingredientsService = ingredientsService;
            this.cloudinaryService = cloudinaryService;
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

            await this.SetRecipeToRecipeCookingMthods(model.CookingMethods, recipe.Id);
            await this.recipesRepository.SaveChangesAsync();
        }

        public T GetById<T>(int recipeId)
        {
            var recipe = this.recipesRepository.All().Where(x => x.Id == recipeId)
                .To<T>().FirstOrDefault();

            return recipe;
        }

        public bool IsExistRecipeTitle(string recipeTtile)
        {
            return this.recipesRepository.All().Any(x => x.Title == recipeTtile);
        }

        private async Task SetRecipeToRecipeCookingMthods(CookingMethodsCheckboxViewModel[] cookingMethodsCheckBox, int recipeId)
        {
            var recipe = await this.recipesRepository.GetByIdWithDeletedAsync(recipeId);

            foreach (var cookingMethodModel in cookingMethodsCheckBox)
            {
                // TODO add validation if null
                if (cookingMethodModel.Selected)
                {
                    var cookingMethod = await this.cookingMethodsRepository.GetByIdWithDeletedAsync(cookingMethodModel.Id);
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
