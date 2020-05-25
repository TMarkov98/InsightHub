﻿using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IIndustryServices
    {
        Task<IndustryModel> CreateIndustry(string name, string imgUrl);
        Task<bool> DeleteIndustry(int id);
        Task<List<IndustryModel>> GetAllIndustries(string sort, string search);
        Task<List<IndustryModel>> GetDeletedIndustries(string search);
        Task<IndustryModel> GetIndustry(int id);
        Task<IndustryModel> UpdateIndustry(int id, string newName, string newImg);
        Task AddSubscription(int userId, int industryId);
        Task RemoveSubscription(int userId, int industryId);
        Task<bool> SubscriptionExists(int userId, int industryId);
    }
}