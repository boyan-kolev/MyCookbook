﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MyCookbook.Common;
using MyCookbook.Data.Models;
using MyCookbook.Data.Models.Enums;

namespace MyCookbook.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [EmailAddress]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(
                AttributesConstraints.UserPasswordMaxLength,
                MinimumLength = AttributesConstraints.UserPasswordMinLength,
                ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повторете паролата")]
            [Compare("Password", ErrorMessage = AttributesErrorMessages.ComparePasswordsErrorMessage)]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(
                AttributesConstraints.UserUsernameMaxLength,
                MinimumLength = AttributesConstraints.UserUsernameMinLength,
                ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
            [Display(Name = "Потребителско име")]
            public string UserName { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(
                AttributesConstraints.UserFirstNameMaxLength,
                MinimumLength = AttributesConstraints.UserFirstNameMinLength,
                ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
            [Display(Name = "Име")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(
                AttributesConstraints.UserLastNameMaxLength,
                MinimumLength = AttributesConstraints.UserLastNameMinLength,
                ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage)]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [DataType(DataType.Date)]
            public DateTime Birthdate { get; set; }

            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            public Gender Gender { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.UserName,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    Birthdate = this.Input.Birthdate,
                    Gender = this.Input.Gender,
                };
                var result = await this._userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this._emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email });
                    }
                    else
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
