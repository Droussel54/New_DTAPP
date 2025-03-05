using New_DTAPP.Data;
using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsersAsync(bool activeOnly = false);
        Task<UserModel?> GetUserByIdAsync(int id);
        Task<UserModel?> AddUserAsync(UserModel model);
        Task<UserModel?> UpdateUserAsync(UserModel model);
        Task<UserModel?> RemoveUserAsync(UserModel model);
        Task<UserModel?> GetUserByUsernameAsync(string userName);
        bool? UserExistsAsync(int id);
        List<string> FindUsersInAD(string userName);
        string FindUserInAD(string userName);
    }
}
