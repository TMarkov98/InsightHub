using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IReportServices
    {
        Task<ReportModel> CreateReport(string title, string summary, string description, string author, string imgUrl, string industry, string tags);
        Task DeleteReport(int id);
        Task<ReportModel> GetReport(int id);
        Task<ICollection<ReportModel>> GetReportsFeatured();
        Task<ICollection<ReportModel>> GetReportsDeleted(string sort, string search);
        Task<ICollection<ReportModel>> GetReportsPending(string sort, string search);
        Task<ICollection<ReportModel>> GetTop5NewReports();
        Task<ICollection<ReportModel>> GetTop5MostDownloads();
        Task<ICollection<ReportModel>> GetReports(string sort, string search, string author, string industry, string tag);
        Task ToggleFeatured(int id);
        Task ApproveReport(int id);
        Task<ReportModel> UpdateReport(int id, string title, string summary, string description, string imgUrl, string industry, string tags);
        Task PermanentlyDeleteReport(int id);
        Task AddToDownloadsCount(int userId, int reportId);

    }
}