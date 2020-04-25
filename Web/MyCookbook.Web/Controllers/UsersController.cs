namespace MyCookbook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Users.MyRecipes;

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
    }
}
