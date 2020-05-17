using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IReportServices
    {
        Task<ReportModel> CreateReport(string title, string description, string author, string industry, string tags);
        Task<bool> DeleteReport(int id);
        Task<ReportModel> GetReport(int id);
        Task<ICollection<ReportModel>> GetReportsFeatured();
        Task<ICollection<ReportModel>> GetReportsDeleted();
        Task<ICollection<ReportModel>> GetReportsPending();
        Task<ICollection<ReportModel>> GetTop5NewReports();
        Task<ICollection<ReportModel>> GetTop5MostDownloads();
        Task<ICollection<ReportModel>> GetReports();
        Task<ReportModel> ToggleFeatured(int id);
        Task<ReportModel> TogglePending(int id);
        Task<ReportModel> UpdateReport(int id, string title, string description, string industry, string tags);
    }
}