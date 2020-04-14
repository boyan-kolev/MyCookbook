namespace MyCookbook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Ratings;

    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService ratingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RatingsController(
            IRatingsService ratingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.ratingsService = ratingsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RatingResponseModel>> Post(RatingInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.ratingsService.RatingAsync(input.RecipeId, userId, input.Stars);
            var ratings = this.ratingsService.GetRatings(input.RecipeId);

            return new RatingResponseModel { Ratings = ratings };
        }
    }
}
