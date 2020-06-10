using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IIndustryServices
    {
        Task<IndustryModel> CreateIndustry(string name, string imgUrl);
        Task DeleteIndustry(int id);
        Task<ICollection<IndustryModel>> GetAllIndustries(string sort, string search);
        Task<ICollection<IndustryModel>> GetDeletedIndustries(string search);
        Task<IndustryModel> GetIndustry(int id);
        Task<IndustryModel> UpdateIndustry(int id, string newName, string newImg);
        Task AddSubscription(int userId, int industryId);
        Task RemoveSubscription(int userId, int industryId);
        Task<bool> SubscriptionExists(int userId, int industryId);
    }
}