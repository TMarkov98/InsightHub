using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IIndustryServices
    {
        Task<IndustryModel> CreateIndustry(string name);
        Task<bool> DeleteIndustry(int id);
        Task<List<IndustryModel>> GetAllIndustries();
        Task<IndustryModel> GetIndustry(int id);
        Task<IndustryModel> UpdateIndustry(int id, string newName);
    }
}