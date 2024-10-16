using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public bool EmailExist(string email)
        {
            bool value = _db.applicationUser.Any(emailId => emailId.Email == email);
            return value;
        }

        public bool UsernameExist(string uid)
        {
            bool value = _db.applicationUser.Any(id => id.Id == uid);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public async Task<ApplicationUser> getUserByUsernameAsync(string username)
        {
            return await _db.applicationUser.FirstOrDefaultAsync(find => find.UserName == username);
        }

        public async Task<ApplicationUser> getUserByEmailAsync(string email)
        {
            return await _db.applicationUser.FirstOrDefaultAsync(find => find.Email == email);
        }

        public async Task<ApplicationUser> getUserByIdAsync(string id)
        {
            return await _db.applicationUser.FirstOrDefaultAsync(find => find.Id == id);
        }

        public bool UpdateAppUser(ApplicationUser application)
        {
            _db.applicationUser.Update(application);
            return Save();
        }
        //Not show user Role: Admin
        public async Task<ICollection<ApplicationUser>> getUserAndCoins()
        {
            var users = await _userManager.Users.ToListAsync();
            List<ApplicationUser> usernameRoles = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains(UserRole.Admin))
                {
                    var username = await getUserByIdAsync(user.Id);
                    usernameRoles.Add(username);
                }
            }

            return usernameRoles;
        }

        public async Task<ICollection<ApplicationUser>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            
            List<ApplicationUser> usernameRoles = new List<ApplicationUser>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                if (!roles.Contains(UserRole.Admin))
                {
                    var username = await getUserByIdAsync(user.Id);
                    usernameRoles.Add(username);
                }
            }
            return usernameRoles;
        }

        public bool Delete(ApplicationUser application)
        {
              _db.applicationUser.Remove(application);
            return Save();
        }

        public bool PhoneExitst(string phone)
        {
            bool value = _db.applicationUser.Any(PhoneNum => PhoneNum.PhoneNumber == phone);
            return value;
        }

        public bool CheckEmailUserUpdate(string email,string id)
        {
            bool value = _db.applicationUser.Any(EmailUser => EmailUser.Email == email && EmailUser.Id != id);
            return value;
        }

        public bool CheckPhoneUserUpdate(string phone, string id)
        {
            bool value = _db.applicationUser.Any(PhoneUser => PhoneUser.PhoneNumber == phone && PhoneUser.Id != id);
            return value;
        }

       
    }
}
