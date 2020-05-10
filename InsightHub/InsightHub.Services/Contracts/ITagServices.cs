using InsightHub.Models;
using InsightHub.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface ITagServices
    {
        Task<TagDTO> CreateTag(string name);
        Task<bool> DeleteTag(int id);
        Task<TagDTO> GetTag(int id);
        Task<ICollection<TagDTO>> GetTags();
        Task<TagDTO> UpdateTag(int id, string name);
        void ValidateTagExists(Tag tag);
    }
}