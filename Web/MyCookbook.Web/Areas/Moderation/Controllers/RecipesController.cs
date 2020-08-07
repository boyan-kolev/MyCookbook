namespace MyCookbook.Web.Areas.Moderation.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Common;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Moderation;

    public class RecipesController : ModerationController
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public IActionResult Manage(int page = 1)
        {
            var recipes = this.recipesService.GetAll<ModerationRecipesNotApproved>(false, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var viewModel = new ModerationRecipesManageViewModel { Recipes = recipes };

            var count = this.recipesService.GetCountOfAllRecipes(false);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Approve(int recipeId)
        {
            await this.recipesService.Approve(recipeId);

            return this.Redirect("/Moderation/Recipes/Manage");
        }

        public async Task<IActionResult> Delete(int recipeId)
        {
            await this.recipesService.DeleteAsync(recipeId);

            return this.Redirect("/Moderation/Recipes/Manage");
        }
    }
}
