using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.DTOs;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class TagServices : ITagServices
    {
        private readonly InsightHubContext _context;
        public async Task<TagDTO> CreateTag(string name)
        {
            var tagDTO = TagMapper.MapDTOFromString(name);

            if (!await _context.Tags.AnyAsync(t => t.Name == name))
            {
                var tag = TagMapper.MapTagFromDTO(tagDTO);
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }

            return tagDTO;
        }

        public async Task<ICollection<TagDTO>> GetTags()
        {
            var tagDTOs = await _context.Tags
                .Include(t => t.Reports)
                .Select(t => TagMapper.MapDTOFromTag(t))
                .ToListAsync();
            return tagDTOs;
        }

        public async Task<TagDTO> GetTag(int id)
        {
            var tag = await _context.Tags
                .Include(t => t.Reports)
                .FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);

            var tagDTO = TagMapper.MapDTOFromTag(tag);
            return tagDTO;
        }

        public async Task<TagDTO> UpdateTag(int id, string name)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);

            tag.Name = name;

            var tagDTO = TagMapper.MapDTOFromTag(tag);
            await _context.SaveChangesAsync();

            return tagDTO;
        }

        public async Task<bool> DeleteTag(int id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null || tag.IsDeleted)
            {
                return false;
            }

            tag.IsDeleted = true;
            tag.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }

        public void ValidateTagExists(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("No tag found.");
        }
    }
}
