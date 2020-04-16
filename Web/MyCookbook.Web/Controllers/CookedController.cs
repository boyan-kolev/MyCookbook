namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Cooked;

    [ApiController]
    [Route("api/[controller]")]
    public class CookedController : ControllerBase
    {
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CookedController(
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CookedResponseModel>> Post(CookedInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isAdded = await this.recipesService.SetRecipeToUserCookedRecipesAsync(input.RecipeId, userId);
            var cookTimes = this.recipesService.GetCookTimes(input.RecipeId);

            var responseModel = new CookedResponseModel
            {
                IsAdded = isAdded,
                CookTimes = cookTimes,
            };

            return responseModel;
        }
    }
}
