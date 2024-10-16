using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface IUserRepository
    {
        Task<ICollection<ApplicationUser>> GetUsers();
        Task<ICollection<ApplicationUser>> getUserAndCoins();
        Task<ApplicationUser> getUserByUsernameAsync(string username);
        Task<ApplicationUser> getUserByEmailAsync(string email);
        Task<ApplicationUser> getUserByIdAsync(string id);
        
        bool EmailExist(string email);
        bool CheckEmailUserUpdate(string email,string id);

        bool PhoneExitst(string phone);
        bool CheckPhoneUserUpdate(string phone,string id);
        bool UsernameExist(string uid);
        bool UpdateAppUser(ApplicationUser application);
        bool Save();
       
        bool Delete(ApplicationUser application);
        
    }
}
