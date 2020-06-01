using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IUserServices
    {
        Task BanUser(int id, string reason);
        Task<List<UserModel>> GetBannedUsers(string search);
        Task<List<UserModel>> GetPendingUsers(string search);
        Task<UserModel> GetUser(int id);
        Task<List<UserModel>> GetUsers(string search);
        Task UnbanUser(int id);
        Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool isBanned, string banReason);
        Task DeleteUser(int id);
        Task ApproveUser(int id);
        Task<List<ReportModel>> GetDownloadedReports(int userId, string search);
        Task<List<ReportModel>> GetUploadedReports(int userId, string search);
        Task<List<IndustryModel>> GetSubscriptions(int userId);
        Task<string> GetSubscribedUsers(string industry);
    }
}