using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class TagServices : ITagServices
    {
        private readonly InsightHubContext _context;
        public TagServices(InsightHubContext context)
        {
            this._context = context ?? throw new ArgumentNullException("Context can NOT be null.");
        }
        public async Task<TagModel> CreateTag(string name)
        {
            name = name.Trim();
            var tagDTO = TagMapper.MapModelFromInput(name);

            if (!await _context.Tags.AnyAsync(t => t.Name.ToLower() == name.ToLower()))
            {
                var tag = TagMapper.MapEntityFromModel(tagDTO);
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();
                tagDTO = TagMapper.MapModelFromEntity(tag);
                return tagDTO;
            }
            throw new ArgumentException($"Tag with name {name} already exists.");
        }

        public async Task<ICollection<TagModel>> GetTags(string sort, string search)
        {
            var tagDTOs = await _context.Tags
                .Where(t => !t.IsDeleted)
                .Include(t => t.Reports)
                .ThenInclude(rt => rt.Report)
                .Select(t => TagMapper.MapModelFromEntity(t))
                .ToListAsync();
            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "name":
                        tagDTOs = tagDTOs.OrderBy(t => t.Name).ToList();
                        break;
                    case "name_desc":
                        tagDTOs = tagDTOs.OrderByDescending(t => t.Name).ToList();
                        break;
                    case "newest":
                        tagDTOs = tagDTOs.OrderByDescending(t => t.CreatedOn).ToList();
                        break;
                    case "oldest":
                        tagDTOs = tagDTOs.OrderBy(t => t.CreatedOn).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (search != null)
            {
                tagDTOs = tagDTOs.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return tagDTOs;
        }

        public async Task<ICollection<TagModel>> GetDeletedTags()
        {
            var tagDTOs = await _context.Tags
                .Where(t => t.IsDeleted)
                .Include(t => t.Reports)
                .ThenInclude(rt => rt.Report)
                .Select(t => TagMapper.MapModelFromEntity(t))
                .ToListAsync();
            return tagDTOs;
        }

        public async Task<TagModel> GetTag(int id)
        {
            var tag = await _context.Tags
                .Where(t => !t.IsDeleted)
                .Include(t => t.Reports)
                .ThenInclude(rt => rt.Report)
                .FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);

            var tagDTO = TagMapper.MapModelFromEntity(tag);
            return tagDTO;
        }

        public async Task<TagModel> UpdateTag(int id, string name)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);

            if (await _context.Tags.AnyAsync(t => t.Name.ToLower() == name.ToLower() && t.Id != id))
            {
                throw new ArgumentException($"Tag with name {name} already exists.");
            }

            tag.Name = name;
            tag.ModifiedOn = DateTime.UtcNow;

            var tagDTO = TagMapper.MapModelFromEntity(tag);
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
            tag.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public void ValidateTagExists(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("No Tag found.");
        }
    }
}
