namespace MyCookbook.Web.Areas.Moderation.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Moderation;

    public class RecipesController : ModerationController
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public IActionResult Manage()
        {
            var recipes = this.recipesService.GetAll<ModerationRecipesNotApproved>(false);
            var viewModel = new ModerationRecipesManageViewModel { Recipes = recipes };

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
