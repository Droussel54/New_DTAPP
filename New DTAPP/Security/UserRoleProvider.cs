using System.DirectoryServices.AccountManagement;

using JT.AspNetBaseRoleProvider;

using Microsoft.EntityFrameworkCore;

using New_DTAPP.Models;

using New_DTAPP.Data;

using static Constants;

using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;
using System.Data;

namespace New_DTAPP.Security
{
    public class UserRoleProvider : IBaseRoleProvider
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly bool isDevelopment;
        private readonly PrincipalContext AD;

        public UserRoleProvider(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentName.Development;
            AD = new(ContextType.Domain, isDevelopment ? DevLdapUrl : ProdLdapUrl);
        }

        public Task<ICollection<string>> GetUserRolesAsync(string userName)
        {
            var result = new List<string>();
            userName = RemoveDomainFromUsername(userName).ToLower();
            using var scope = scopeFactory.CreateScope();
            NewDtappContext db = scope.ServiceProvider.GetRequiredService<NewDtappContext>();

            // Finds only enabled users matching the username
            Data.User? user = db.Users.Where(i => i.Disabled == false && i.Username.ToLower() == userName)
                .Include(i => i.Role).ToList().FirstOrDefault();
            if (user != null)
            {
                result.Add(user.Role.RoleName);
            }
            /** else if (user == null)
            {
                Data.User newUser = FindUserInAD(userName);
                Data.Role? userRole = db.Roles.Where(r => r.RoleName == Constants.User).FirstOrDefault();
                if (userRole != null)
                {
                    newUser.Role = userRole;
                    db.Add(newUser);
                    db.SaveChanges();
                }
            }**/

            return Task.FromResult((ICollection<string>)result);
        }

        public static string RemoveDomainFromUsername(string username)
        {
            if (username.Contains('\\'))
            {
                return username.Split('\\')[1];
            }
            return username;
        }

        public Data.User FindUserInAD(string userName)
        {
            UserPrincipal userPrincipal = new(AD)
            {
                SamAccountName = userName
            };
            PrincipalSearcher search = new(userPrincipal);
            UserPrincipal foundUser = (UserPrincipal)search.FindOne();
            search.Dispose();
            Data.User newUser = isDevelopment
                ? new()
                {
                    Username = userName,
                    FirstName = foundUser.GivenName,
                    LastName = foundUser.Surname,
                    Email = foundUser.UserPrincipalName
                }
                : new()
                {
                    Username = userName,
                    FirstName = foundUser.GivenName,
                    LastName = foundUser.Surname,
                    Email = foundUser.EmailAddress
                };
            return newUser;
        }
    }
}