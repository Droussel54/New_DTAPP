using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using New_DTAPP.Controllers;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using Newtonsoft.Json;
using System.DirectoryServices.AccountManagement;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using static Constants;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace New_DTAPP.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;
        private bool isDevelopment;
        private PrincipalContext AD;
        public UserRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentName.Development;
            //AD = new(ContextType.Domain, isDevelopment ? DevLdapUrl : ProdLdapUrl);
        }

        public async Task<UserModel?> AddUserAsync(UserModel userModel)
        {
            User user = new User();
            if (userModel != null)
            {
                user = userModel.ModelToEntityUser();
                _context.Users.Add(user);
                _context.SaveChangesAsync();
                return userModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserModel>> GetAllUsersAsync(bool excludeArchive)
        {
           
            List<UserModel> model = new();
            if (excludeArchive)
            {
                var users = await _context.Users.Where(x => x.Disabled != true).Include(t => t.Role).OrderBy(u => u.Username).ToListAsync();
                model = users.EntityToModelUser()!;
            }
            else
            {
                var users = await _context.Users.Include(t => t.Role).OrderBy(u => u.Username).ToListAsync();
                model = users.EntityToModelUser()!;
            }
            
            return model;
        }

        public async Task<UserModel?> GetUserByIdAsync(int id)
        {
            UserModel model = new();

            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserId == id);

            model = user?.EntityToModelUser()!;

            return model;
        }

        public async Task<UserModel?> UpdateUserAsync(UserModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserId == model.UserId);

            if (user != null)
            {

                user.Username = model.Username;
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.Email = model.Email;
                user.Alias = model.Alias;
                user.Disabled= model.Disabled;
                user.RoleId = model.RoleId;

                _context.Users.Update(user);

                await _context.SaveChangesAsync();

                return user?.EntityToModelUser()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<UserModel?> RemoveUserAsync(UserModel userModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserId == userModel.UserId);

            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();

                return user?.EntityToModelUser()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<UserModel?> GetUserByUsernameAsync(string userName)
        {
            UserModel model = new();

            var user = await _context.Users.FirstOrDefaultAsync(t => t.Username == userName);

            model = user?.EntityToModelUser()!;

            return model;
        }

        public bool? UserExistsAsync(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }

        public List<string> FindUsersInAD(string userName)
        {
            List<string> existingUsernames = _context.Users.Select(u => u.Username).ToList();
            UserPrincipal userPrincipal = new(AD)
            {
                SamAccountName = "*" + userName + "*"
            };
            PrincipalSearcher search = new(userPrincipal);
            List<string> potentialUsernames = search.FindAll().AsQueryable().Select(user => user.SamAccountName).Take(50).ToList();
            search.Dispose();
            if (!potentialUsernames.Any())
            {
                return potentialUsernames;
            }
            potentialUsernames.RemoveAll(item => existingUsernames.Contains(item));
            return potentialUsernames;
        }

        public string FindUserInAD(string userName)
        {
            string jsonUser;
            UserPrincipal userPrincipal = new(AD)
            {
                SamAccountName = userName
            };
            PrincipalSearcher search = new(userPrincipal);
            UserPrincipal foundUser = (UserPrincipal)search.FindOne();
            search.Dispose();
            if (isDevelopment)
            {
                jsonUser = JsonConvert.SerializeObject(new
                {
                    FirstName = foundUser.GivenName,
                    LastName = foundUser.Surname,
                    Email = foundUser.UserPrincipalName
                });
            }
            else
            {
                jsonUser = JsonConvert.SerializeObject(new
                {
                    FirstName = foundUser.GivenName,
                    LastName = foundUser.Surname,
                    Email = foundUser.EmailAddress
                });
            }
            return jsonUser;
        }

    }
}
