using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface ICoinRepository
    {
        Task<ICollection<CoinProducts>> GetCoins();

        Task<CoinProducts> GetCoin(int id);
    }
}
