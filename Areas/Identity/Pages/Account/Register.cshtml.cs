using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using Project.Models;
using Project.Repositorys.IRepository;
using Project.Utilities;

namespace Project.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userReponsitory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IEmailService emailService,
            IUserRepository userReponsitory,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _emailService = emailService;
            _userReponsitory = userReponsitory;
            _webHostEnvironment = webHostEnvironment;
            _userReponsitory = userReponsitory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [StringLength(50, MinimumLength = 8, ErrorMessage = "Not Valid (>=8 characters)")]
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string PhoneNumber { get; set; }
            public string city { get; set; }
            public string district { get; set; }
            public string ward { get; set; }
            public int coin { get; set; }
            public string Image { get; set; }

        }

        public class InputModelValidator : AbstractValidator<InputModel>
        {
            public InputModelValidator()
            {
                RuleFor(x => x.PhoneNumber).NotNull().Matches(@"\+(84[3|5|7|8|9])+([0-9]{8})\b")
                    .WithMessage("Enter correct format, start with +84");
            }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var role = Request.Form["role"].ToString();
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if (_userReponsitory.EmailExist(Input.Email))
                {
                    TempData.Remove("TempMessage");
                    TempData["TempMessage"] = "This email is already exist, try another one!";
                    return Page();
                }

                if (_userReponsitory.UsernameExist(Input.Email))
                {
                    TempData.Remove("TempMessage");
                    TempData["TempMessage"] = "This Username is already exist, try another one!";
                    return Page();
                }


                var user = new ApplicationUser { LastName = Input.LastName, FirtsName = Input.FirstName, UserName = Input.Username.ToLower(), Email = Input.Email.ToLower(), coin = 0, city = Input.city, district = Input.district, ward = Input.ward, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(role))
                    {
                        role = UserRole.Customers;
                    }
                    await _userManager.AddToRoleAsync(user, role);

                    var webroot = _webHostEnvironment.WebRootPath;
                    var files = Request.Form.Files;

                    var userFromDb = await _userReponsitory.getUserByEmailAsync(Input.Email);


                    if (files.Count > 0)
                    {
                        var extension = Path.GetExtension(files[0].FileName);
                        var destinationUserImage = Path.Combine(webroot, @"images\User") + @$"\{userFromDb.Id}{extension}";
                        await using (var fileStream = new FileStream(destinationUserImage, FileMode.Create))
                        {
                            await files[0].CopyToAsync(fileStream);
                        }
                        userFromDb.Image = @"\images\User\" + $"{userFromDb.Id}{extension}";
                    }
                    else
                    {
                        //var defaultUserImage = Path.Combine(webroot, @"images\Default") + $"\\{StaticData.DefaultUserImage}";
                        //var destinationUserImage = Path.Combine(webroot, @"images\User") + @$"\{userFromDb.Id}.jpg";
                        //System.IO.File.Copy(defaultUserImage, destinationUserImage);
                        //userFromDb.Image = @$"\images\User\{userFromDb.Id}.jpg";
                        userFromDb.Image = StaticData.DefaultUserImage;
                    }

                    _userReponsitory.Save();


                    _logger.LogInformation("User created a new account with password.");
                    if (role == UserRole.Customers)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                        var link = hostname + Url.Action("ConfirmEmail",
                            "Email", new { area = "Customers", userId = user.Id, code });
                        var message = $"Link Confirm email: <a href=\"{link}\">Here</a>";
                        await _emailSender.SendEmailAsync(user.Email, user.LastName, message);
                        return RedirectToAction("EmailConfirmSuccess", "Email", new { area = "Customers" });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }


                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
