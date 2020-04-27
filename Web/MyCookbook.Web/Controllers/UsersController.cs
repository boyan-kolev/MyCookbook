namespace MyCookbook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Users.Cooked;
    using MyCookbook.Web.ViewModels.Users.Favorites;
    using MyCookbook.Web.ViewModels.Users.MyRecipes;
    using MyCookbook.Web.ViewModels.Users.Profile;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult MyRecipes()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetById<UsersMyRecipesViewModel>(userId);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Favorites()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetById<UsersFavoritesViewModel>(userId);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Cooked()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetById<UsersCookedViewModel>(userId);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.usersService.GetById<UsersMyProfileViewModel>(userId);

            return this.View(viewModel);
        }
    }
}
