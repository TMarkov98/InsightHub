using InsightHub.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IReportServices
    {
        Task<ReportDTO> CreateReport(string title, string description, string author, string industry, string tags);
        Task<bool> DeleteReport(int id);
        Task<ReportDTO> GetReport(int id);
        Task<ICollection<ReportDTO>> GetReports();
        Task<ReportDTO> ToggleFeatured(int id);
        Task<ReportDTO> TogglePending(int id);
        Task<ReportDTO> UpdateReport(int id, string title, string description, string industry, string tags);
    }
}