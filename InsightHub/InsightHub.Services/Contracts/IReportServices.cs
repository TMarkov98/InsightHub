using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IReportServices
    {
        Task<ReportModel> CreateReport(string title, string summary, string description, string author, string imgUrl, string industry, string tags);
        Task<bool> DeleteReport(int id);
        Task<ReportModel> GetReport(int id);
        Task<ICollection<ReportModel>> GetReportsFeatured();
        Task<ICollection<ReportModel>> GetReportsDeleted();
        Task<ICollection<ReportModel>> GetReportsPending();
        Task<ICollection<ReportModel>> GetTop5NewReports();
        Task<ICollection<ReportModel>> GetTop5MostDownloads();
        Task<ICollection<ReportModel>> GetReports(string sort, string search, string author, string industry, string tag);
        Task<ReportModel> ToggleFeatured(int id);
        Task<ReportModel> TogglePending(int id);
        Task<ReportModel> UpdateReport(int id, string title, string summary, string description, string imgUrl, string industry, string tags);
        Task AddToDownloadsCount(int userId, int reportId);
    }
}