namespace MyCookbook.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MyCookbook.Common;
    using MyCookbook.Data.Models;
    using MyCookbook.Services.Data.Contracts;
    using MyCookbook.Web.Infrastructure.ValidationAttributes;

    public class ProfilePicture : PageModel
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfilePicture(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public string ProfilePictureUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [Display(Name = "Нова профилна снимка")]
            [DataType(DataType.Upload)]
            [MaxFileSize(AttributesConstraints.RecipeImageMaxSize)]
            [AllowedExtensions(new string[] { ".jpeg", ".jpg", "png" })]
            public IFormFile NewProfilePicture { get; set; }
        }

        private void Load()
        {
            var userId = this.userManager.GetUserId(this.User);
            var profilePictureUrl = this.usersService.GetProfilePictureUrl(userId);
            this.ProfilePictureUrl = profilePictureUrl;
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
            await this.usersService.ChangeProfilePictureAsync(userId, this.Input.NewProfilePicture);

            return this.RedirectToPage();
        }
    }
}
