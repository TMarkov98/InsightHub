using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IUserServices
    {
        Task BanUser(int id, string reason);
        Task<ICollection<UserModel>> GetBannedUsers(string search);
        Task<ICollection<UserModel>> GetPendingUsers(string search);
        Task<UserModel> GetUser(int id);
        Task<ICollection<UserModel>> GetUsers(string search);
        Task UnbanUser(int id);
        Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool isBanned, string banReason);
        Task DeleteUser(int id);
        Task ApproveUser(int id);
        Task<ICollection<ReportModel>> GetDownloadedReports(int userId, string search);
        Task<ICollection<ReportModel>> GetUploadedReports(int userId, string search);
        Task<ICollection<IndustryModel>> GetSubscriptions(int userId);
        Task<string> GetSubscribedUsers(string industry);
    }
}