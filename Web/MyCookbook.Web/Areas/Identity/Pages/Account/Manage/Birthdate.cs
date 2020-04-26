namespace MyCookbook.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class Birthdate : PageModel
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public Birthdate(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [DataType(DataType.Date)]
        [Display(Name = "Рождена дата")]
        public DateTime UserBirthdate { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [DataType(DataType.Date)]
            [Display(Name = "Нова рождена дата")]
            public DateTime NewUserBirthdate { get; set; }
        }

        private void Load()
        {
            var userId = this.userManager.GetUserId(this.User);
            var userBirthdate = this.usersService.GetBirthdate(userId);
            this.UserBirthdate = userBirthdate;
        }

        public IActionResult OnGet()
        {
            this.Load();

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                this.Load();
                return this.Page();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.usersService.ChangeBirthdate(userId, this.Input.NewUserBirthdate);

            return this.RedirectToPage();
        }
    }
}
