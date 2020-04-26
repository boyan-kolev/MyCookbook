namespace MyCookbook.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;

    public class FirstLastName : PageModel
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public FirstLastName(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Display(Name = "име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(
                AttributesConstraints.UserFirstNameMaxLength,
                MinimumLength = AttributesConstraints.UserFirstNameMinLength,
                ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
            [Display(Name = "Ново име")]
            public string NewFirstName { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(
                AttributesConstraints.UserLastNameMaxLength,
                MinimumLength = AttributesConstraints.UserLastNameMinLength,
                ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
            [Display(Name = "Нова фамилия")]
            public string NewLastName { get; set; }
        }

        private void Load()
        {
            var userId = this.userManager.GetUserId(this.User);
            var firstAndLastName = this.usersService.GetFirstAndLastName(userId);
            this.FirstName = firstAndLastName.FirstName;
            this.LastName = firstAndLastName.LastName;
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
            await this.usersService.ChangeFirstAndLastNameAsync(userId, this.Input.NewFirstName, this.Input.NewLastName);

            return this.RedirectToPage();
        }
    }
}
