using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using Project.Extensions;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.DtosForAdmin;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles =UserRole.Admin+","+ UserRole.Moderate)]
    public class UserController : Controller
    {
        
        private readonly IUserRepository _iup;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        
        public UserController(IUserRepository iup
            ,IMapper mapper
            , UserManager<IdentityUser> userManager
            )
        {
            
            _iup = iup;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ApplicationUser> users = await _iup.GetUsers();
            var results = _mapper.Map<List<ApplicationUserDtos>>(users);
            return await Task.Run(()=>View(results));
        }
       
        
        public async Task<IActionResult> EditUser(string id)
        {
            var product = await _iup.getUserByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            var ojProduct = _mapper.Map<UpdateUserDtos>(product);
            return await Task.Run(() => View(ojProduct));
        }
        [HttpPost]      
        public async Task<IActionResult> EditUser(UpdateUserDtos applicationUserDtos)
        {
            if (!ModelState.IsValid)
                return await Task.Run(() => View());

            if (applicationUserDtos == null)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser userPro =await _iup.getUserByIdAsync(applicationUserDtos.Id);

            IdentityUser username = await _userManager.FindByIdAsync(userPro.Id);
            //Random rng = new Random(); // Declare this once somewhere - don't keep redeclaring it!
            //var value = 100000000 + rng.NextDouble() * (1000000000 - 100000000);
            if (applicationUserDtos.Role == UserRole.Admin)
            {
                await _userManager.AddToRoleAsync(username, UserRole.Admin);
            }
            else if (applicationUserDtos.Role == UserRole.Moderate)
            {
                await _userManager.AddToRoleAsync(username, UserRole.Moderate);
            }
            else
            {
                await _userManager.AddToRoleAsync(username, UserRole.Customers);
            }
            if (applicationUserDtos.PassWord == null)
            {
                userPro.PasswordHash = userPro.PasswordHash;
            }
            else
            {
                userPro.PasswordHash = _userManager.PasswordHasher.HashPassword(username, applicationUserDtos.PassWord);
            }
            if (userPro.LockoutEnabled == true)
            {

                userPro.LockoutEnd = DateTime.Now.AddDays(7);
            }
            if(userPro.LockoutEnabled == false)
            {
                
                userPro.LockoutEnd = null;
            }
                userPro.Image = StaticData.DefaultUserImage;
            _mapper.Map(applicationUserDtos, userPro);

            if (_iup.CheckEmailUserUpdate(userPro.Email, userPro.Id))
            {
                return BadRequest("Email exist");
            }
            if (_iup.CheckPhoneUserUpdate(userPro.PhoneNumber, userPro.Id))
            {
                return BadRequest("PhoneNumber exist");
            }
            if (!_iup.UpdateAppUser(userPro))
            {
                return BadRequest("Not Found Id User");
            }

            return await Task.Run(() => RedirectToAction("Index"));
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            ApplicationUser userDel = await _iup.getUserByIdAsync(Id);
            if (!_iup.Delete(userDel))
                return BadRequest("Can Delete");
            return await Task.Run(() => RedirectToAction("Index"));
        }
        //[HttpPost,ActionName("Dentail")]
        //public async Task<IActionResult> Dentail(string Id)
        //{
        //    ApplicationUser userDentail = await _iup.getUserByIdAsync(Id);
        //    var result = _mapper.Map<List<UpdateUserDtos>>(userDentail);
        //    return await Task.Run(() => View(result));
        //}
        public async Task<IActionResult> Details(string id)
        {
            var user = await _iup.getUserByIdAsync(id);
            if (user == null)
            {
                return await Task.Run(() => View());
            }
            var ojUser = _mapper.Map<ApplicationUserDtos>(user);
            return await Task.Run(() => View(ojUser));
        }
        public async Task<IActionResult> CreateUser()
        {
            return await Task.Run(() => View());
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(ApplicationUserDtos applicationUser)
        {

            applicationUser.EmailConfirmed = true;
          
            var user = new ApplicationUser {UserName = applicationUser.UserName
                , EmailConfirmed = applicationUser.EmailConfirmed
                ,Email = CreateGuildNameExtensions.NameFileNewGuild()+"@gmail.com"
                ,FirtsName = applicationUser.FirtsName
                ,LastName = applicationUser.LastName,
                Image = StaticData.DefaultUserImage,
        };
            
            await _userManager.CreateAsync(user, applicationUser.PassWord);
            IdentityUser username = await _userManager.FindByNameAsync(applicationUser.UserName);
            await _userManager.AddToRoleAsync(username, UserRole.Customers);
            return await Task.Run(() => RedirectToAction("Index"));

        }
    }
}
