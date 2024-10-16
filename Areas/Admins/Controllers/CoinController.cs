using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Extensions;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.DtosForAdmin;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = UserRole.Admin+ "," + UserRole.Moderate)]
    public class CoinController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISaleCoinRepository _saleCoinRepository;
        private readonly IMapper _mapper;

        public CoinController(IUserRepository userRepository, ISaleCoinRepository saleCoinRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _saleCoinRepository = saleCoinRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _userRepository.getUserAndCoins();
            var result = _mapper.Map<List<ApplicationUserDtos>>(model);
            return await Task.Run(() => View(result));
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser xu = await _userRepository.getUserByIdAsync(id);
            if (xu == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<UpdateCoinDtos>(xu);
            return await Task.Run(() => View(objDto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCoinDtos updateCoinDtos)
        {
            
            if (!ModelState.IsValid)
                return await Task.Run(() => View());

            if (updateCoinDtos == null)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser username = await _userRepository.getUserByIdAsync(updateCoinDtos.Id);
            updateCoinDtos.UserName = username.UserName;
            _mapper.Map(updateCoinDtos, username);

            if (!_userRepository.UpdateAppUser(username))
            {
                ModelState.AddModelError("", $"Something went wrong when Updating the record");
                return StatusCode(500, ModelState);
            }
            return await Task.Run(() => RedirectToAction(nameof(Index)));
        }        
    }
}
