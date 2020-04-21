namespace MyCookbook.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.ViewComponents;

    public class LastCreatedRecipesViewComponent : ViewComponent
    {
        private readonly IRecipesService recipesService;

        public LastCreatedRecipesViewComponent(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public IViewComponentResult Invoke(int count)
        {
            var lastCreatedRecipes = this.recipesService.GetLastCreatedRecipes<LastCreatedRecipeViewModel>(count);
            var viewModel = new LastCreatedRecipesViewModel
            {
                Recipes = lastCreatedRecipes,
            };

            return this.View(viewModel);
        }
    }
}
