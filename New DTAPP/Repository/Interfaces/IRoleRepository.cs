using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface IRoleRepository 
    {
        Task<List<RoleModel>> GetAllRolesAsync();
        Task<RoleModel?> GetRoleByIdAsync(int id);
        Task<RoleModel?> AddRoleAsync(RoleModel model);
        Task<RoleModel?> UpdateRoleAsync(RoleModel model);
        Task<RoleModel?> RemoveRoleAsync(RoleModel model);
    }
}
