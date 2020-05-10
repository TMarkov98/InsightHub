using InsightHub.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IIndustryServices
    {
        Task<IndustryDTO> CreateIndustry(string name);
        Task<bool> DeleteIndustry(int id);
        Task<List<IndustryDTO>> GetAllIndustries();
        Task<IndustryDTO> GetIndustry(int id);
        Task<IndustryDTO> UpdateIndustry(int id, string newName);
    }
}