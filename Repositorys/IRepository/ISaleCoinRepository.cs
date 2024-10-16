using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface ISaleCoinRepository
    {
        Task<ICollection<SaleCoin>> GetSaleCoins(string uid);
        Task<SaleCoin> GetSaleCoin(string id, string uid);
        Task<ICollection<SaleCoin>> GetSaleCoinsAdmin();
        Task<SaleCoin> GetSaleCoinForAdmin(string id);
        bool SaleCoinIdExist(string id);
        bool CreateSaleCoin(SaleCoin saleCoin);
        bool UpdateSaleCoin(SaleCoin saleCoin);
        bool DeleteSaleCoin(SaleCoin saleCoin);
        bool Save();
    }
}
