using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.DtosForAdmin;
using Project.Models.DtosForAdmin.ViewModels;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Moderate)]
    public class DashboardController : Controller
    {
        private readonly IDashBoardRespository _idashboard;
        
        public DashboardController(IDashBoardRespository dashBoardRespository)
        {
            _idashboard = dashBoardRespository;
        }

        public async Task<IActionResult> Index()
        {
            List<int> getProductChartDtos = new List<int>();
            List<double> getCoinChartDtos = new List<double>();
           
            int CountUserOneMonth = 0;
            double SumCointChartDtos = 0;
            int CountUser =await _idashboard.GetCountUser();
            int CountPro =await _idashboard.GetCountProduct();
            double CountProductPrepare = await _idashboard.GetProductStatusPrepare();
            CountProductPrepare = CountProductPrepare*100 / CountPro;
            double CountProductSrart = await _idashboard.GetProductStatusStart();
            CountProductSrart = CountProductSrart*100 / CountPro;
            double CountProductEnd = await _idashboard.GetProductStatusEnd();
            CountProductEnd = CountProductEnd*100 / CountPro;
            double CountProductAccept = await _idashboard.GetProductStatusAccept();
            CountProductAccept = CountProductAccept*100 / CountPro;
            double CountCoint = await _idashboard.GetCountCoint();
            double CountTurnover = await _idashboard.GetTurnover();
            for (int i = 1; i <= 12; i++)
            {
                CountUserOneMonth = await _idashboard.GetCountUserOneMonth(i);
                getProductChartDtos.Add(CountUserOneMonth);
                SumCointChartDtos = await _idashboard.GetCountCoinOneMonth(i);
                getCoinChartDtos.Add(SumCointChartDtos);
            }
           
            ViewCountVM viewCount = new ViewCountVM {
                CountUser = CountUser,
                CountProduct = CountPro,
                CountOrder = CountCoint,
                CountEmail = CountTurnover,
                productChartDtos=getProductChartDtos,
                CoinChartDtos =getCoinChartDtos,
                productStatusPrepare=CountProductPrepare,
                productStatusStart= CountProductSrart,
                productStatusEnd= CountProductEnd,
                productStatusAccept= CountProductAccept,
                applicationUsers= await _idashboard.GetTopUser()
        };
            return await Task.Run(() => View(viewCount));
        }
        
        
    }
}
