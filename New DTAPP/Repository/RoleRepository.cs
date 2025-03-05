using Microsoft.EntityFrameworkCore;
using New_DTAPP.Controllers;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace New_DTAPP.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;

        public RoleRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<RoleModel?> AddRoleAsync(RoleModel model)
        {

            Data.Role role = new Data.Role();

            if (role != null)
            {
                role.RoleName = model.RoleName;
                role.Ordering = model.Ordering;

                _context.Roles.Add(role);

                await _context.SaveChangesAsync();

                return role.EntityToModelRole()!;
            }
            else
            {
                return null;
            }
        }


        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            List<RoleModel> model = new();

            var roles = await _context.Roles.ToListAsync();

            model = roles.EntityToModelRole()!;

            return model;
        }

        public async Task<RoleModel?> GetRoleByIdAsync(int id)
        {
            RoleModel model = new();

            var role = await _context.Roles.FirstOrDefaultAsync(t => t.RoleId == id);

            model = role?.EntityToModelRole()!;

            return model;
        }

        public async Task<RoleModel?> UpdateRoleAsync(RoleModel roleModel)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(t => t.RoleId == roleModel.RoleId);

            if (role != null)
            {

                role.RoleName = roleModel.RoleName;
                role.Ordering = roleModel.Ordering;

                //TODO: Test this.
                _context.Set<Role>().Update(role);

                await _context.SaveChangesAsync();

                return role?.EntityToModelRole()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<RoleModel?> RemoveRoleAsync(RoleModel roleModel)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(t => t.RoleId == roleModel.RoleId);

            if (role != null)
            {
                _context.Roles.Remove(role);

                await _context.SaveChangesAsync();

                return role?.EntityToModelRole()!;

            }
            else
            {
                return null;
            }
        }
    }
}
