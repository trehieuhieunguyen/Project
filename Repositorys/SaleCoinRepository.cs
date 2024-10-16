using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Project.Repositorys
{
    public class SaleCoinRepository : ISaleCoinRepository
    {
        private readonly ApplicationDbContext _db;

        public SaleCoinRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateSaleCoin(SaleCoin saleCoin)
        {
            _db.saleCoins.Add(saleCoin);
            return Save();
        }

        public bool DeleteSaleCoin(SaleCoin saleCoin)
        {
            _db.saleCoins.Remove(saleCoin);
            return Save();
        }

        public async Task<SaleCoin> GetSaleCoin(string id, string uid)
        {
            return await _db.saleCoins.Where(u => u.ApplicationUser.Id == uid)
                .Include(u => u.ApplicationUser).AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SaleCoin> GetSaleCoinForAdmin(string id)
{
            return await _db.saleCoins.Include(u => u.ApplicationUser).AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<SaleCoin>> GetSaleCoins(string uid)
        {
            return await _db.saleCoins.Where(u => u.ApplicationUser.Id == uid)
                .Include(u => u.ApplicationUser)
                .OrderByDescending(d => d.DateCreate).ToListAsync();
        }

        public async Task<ICollection<SaleCoin>> GetSaleCoinsAdmin()
        {
            return await _db.saleCoins.Include(u => u.ApplicationUser).Where(c => c.TransactionStatus == false).OrderBy(i => i.DateCreate).ToListAsync();
        }

        public bool SaleCoinIdExist(string id)
        {
            bool value = _db.saleCoins.Any(find => find.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool UpdateSaleCoin(SaleCoin saleCoin)
        {
            _db.saleCoins.Update(saleCoin);
            return Save();
        }
    }
}
