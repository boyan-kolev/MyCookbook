namespace MyCookbook.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.ViewModels.Administration.Moderators.Add;
    using MyCookbook.Web.ViewModels.Administration.Moderators.Remove;

    public class ModeratorsController : AdministrationController
    {
        private const string SuccessAddUsersToModeratorRole = "Избраните потребители бяха добавени успешно като модератори!";
        private const string SuccessRemoveUsersFromModeratorRole = "Избраните модератори бяха премахнати успешно!";
        private readonly IUsersService usersService;

        public ModeratorsController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Add()
        {
            var users = this.usersService.GetAllUsers<UsersListViewModel>();
            var viewModel = new ModeratorsAddViewModel { Users = users };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ModeratorsAddViewModel input)
        {
            foreach (var user in input.Users)
            {
                if (user.Selected)
                {
                    await this.usersService.AddToModeratorRoleAsync(user.Id);
                }
            }

            this.TempData["SuccessEditRecipe"] = SuccessAddUsersToModeratorRole;

            return this.Redirect("/");
        }

        public IActionResult Remove()
        {
            var moderators = this.usersService.GetAllModerators<ModeratorsListViewModel>();
            var viewModel = new ModeratorsRemoveViewModel { Moderators = moderators };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(ModeratorsRemoveViewModel input)
        {
            foreach (var moderator in input.Moderators)
            {
                if (moderator.Selected)
                {
                    await this.usersService.RemoveFromModeratorRoleAsync(moderator.Id);
                }
            }

            this.TempData["SuccessEditRecipe"] = SuccessRemoveUsersFromModeratorRole;

            return this.Redirect("/");
        }
    }
}
