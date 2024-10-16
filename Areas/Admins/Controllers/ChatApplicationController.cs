using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace Project.Areas.Admins.Controllers
{

    [Area("Admins")]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Moderate)]
    public class ChatApplicationController : Controller
    {
      
        public async  Task<IActionResult> Index()
        {
           
            return await Task.Run(() => View());
        }
    }
}
