namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Favorites;

    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public FavoritesController(
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> Post(FavoriteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isAdded = await this.recipesService.SetRecipeToUserFavoriteRecipesAsync(input.RecipeId, userId);

            return isAdded;
        }
    }
}
