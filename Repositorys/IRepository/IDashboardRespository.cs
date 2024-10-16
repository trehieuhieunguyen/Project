using Project.Models;
using Project.Models.DtosForAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface IDashBoardRespository
    {
        Task<int> GetCountUser();
        Task<int> GetCountProduct();
        Task<double> GetCountCoint();
        Task<double> GetTurnover();
        Task<int> GetProductStatusPrepare();
        Task<int> GetProductStatusStart();
        Task<int> GetProductStatusEnd();
        Task<int> GetProductStatusAccept();
        Task<int> GetCountUserOneMonth(int i);
        Task<double> GetCountCoinOneMonth(int i);
        Task<ICollection<ViewTopVM>> GetTopUser();
    }
}
