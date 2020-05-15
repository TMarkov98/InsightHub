using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public interface IUserServices
    {
        Task<bool> BanUser(int id, string reason);
        Task<List<UserModel>> GetBannedUsers();
        Task<List<UserModel>> GetPendingUsers();
        Task<UserModel> GetUser(int id);
        Task<List<UserModel>> GetUsers();
        Task<bool> UnbanUser(int id);
        Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool lockoutEnabled, string LockOutReason);
    }
}