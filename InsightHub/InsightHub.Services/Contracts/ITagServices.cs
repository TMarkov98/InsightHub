using InsightHub.Data.Entities;
using InsightHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface ITagServices
    {
        Task<TagModel> CreateTag(string name);
        Task<bool> DeleteTag(int id);
        Task<TagModel> GetTag(int id);
        Task<ICollection<TagModel>> GetTags(string sort, string search);
        Task<TagModel> UpdateTag(int id, string name);
        void ValidateTagExists(Tag tag);
    }
}