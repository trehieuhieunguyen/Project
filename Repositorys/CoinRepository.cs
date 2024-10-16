using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ApplicationDbContext _db;

        public CoinRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CoinProducts> GetCoin(int id)
        {
            return await _db.coinProducts.FirstOrDefaultAsync(find => find.Id == id);
        }

        public async Task<ICollection<CoinProducts>> GetCoins()
        {
            return await _db.coinProducts.OrderBy(i => i.Id).ToListAsync();
        }

    }
}
