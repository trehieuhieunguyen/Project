using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Project.Repositorys.IRepository;
using FluentValidation;
using Project.Utilities;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace Project.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserRepository _userRepons;
        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IUserRepository userRepons)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userRepons = userRepons;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            //[Required]
            //[EmailAddress]
            //public string Email { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class InputValidator : AbstractValidator<InputModel>
        {
            public InputValidator()
            {
                RuleFor(i => i.Password).Length(6, 100);
                RuleFor(i => i.Username).Length(6, 100);
            }
        }
       
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userRepons.getUserByUsernameAsync(Input.Username);


                    _logger.LogInformation("User logged in.");
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(UserRole.Admin))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admins" });
                    }

                    return RedirectToAction("Index", "Home", new { area = "Customers" });
                    //return LocalRedirect(returnUrl);

                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    //return Page();
                    TempData["Invalid"] = "Tài Khoản hoặc mật khẩu sai.";
                    return RedirectToAction("Index", "Home",new { area = "Customers"});
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();

        }
    }
}
