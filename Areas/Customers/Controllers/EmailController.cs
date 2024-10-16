using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Route("[controller]/")]
    public class EmailController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailService;


        public EmailController(UserManager<IdentityUser> userManage, IEmailSender emailService)
        {
            _userManager = userManage;
            _emailService = emailService;
        }
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }
        [Route("EmailConfirmSuccess")]
        public async Task<IActionResult> EmailConfirmSuccess()
        {
            return await Task.Run(() => View());
        }
    }
}
