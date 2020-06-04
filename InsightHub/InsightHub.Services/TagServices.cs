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

        /// <summary>
        /// Creates a new Tag in the context.
        /// </summary>
        /// <param name="name">The name of the new Tag.</param>
        /// <returns>Tag Model on success. Throws ArgumentException if a Tag with this name already exists.</returns>
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

        /// <summary>
        /// Lists all active Tags from the context. Tags must not be soft-deleted.
        /// </summary>
        /// <param name="sort">The property to sort the resulting tags by.</param>
        /// <param name="search">Searches tags by their name.</param>
        /// <returns>ICollection of Tag Models.</returns>
        public async Task<ICollection<TagModel>> GetTags(string sort, string search)
        {
            var tagModels = await _context.Tags
                .Where(t => !t.IsDeleted)
                .Include(t => t.ReportTags)
                .ThenInclude(rt => rt.Report)
                .Select(t => TagMapper.MapModelFromEntity(t))
                .ToListAsync();

            tagModels = SortTags(sort, tagModels).ToList();

            tagModels = SearchTags(search, tagModels).ToList();

            return tagModels;
        }

        /// <summary>
        /// Lists all soft-deleted tags from the context.
        /// </summary>
        /// <returns>ICollection of TagModels</returns>
        public async Task<ICollection<TagModel>> GetDeletedTags()
        {
            var tagDTOs = await _context.Tags
                .Where(t => t.IsDeleted)
                .Include(t => t.ReportTags)
                .ThenInclude(rt => rt.Report)
                .Select(t => TagMapper.MapModelFromEntity(t))
                .ToListAsync();
            return tagDTOs;
        }

        /// <summary>
        /// Gets a tag from the context by its ID.
        /// </summary>
        /// <param name="id">The ID of the target Tag.</param>
        /// <returns>Tag Model on success. Throws ArgumentNullException if Tag does not exist.</returns>
        public async Task<TagModel> GetTag(int id)
        {
            var tag = await _context.Tags
                .Include(t => t.ReportTags)
                .ThenInclude(rt => rt.Report)
                .FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);

            var tagDTO = TagMapper.MapModelFromEntity(tag);
            return tagDTO;
        }

        /// <summary>
        /// Updates the properties of an existing Tag.
        /// </summary>
        /// <param name="id">The ID of the target Tag.</param>
        /// <param name="name">The new Name of the Tag.</param>
        /// <returns>Throws ArgumentNullException if Tag does not exist. Throws ArgumentException if a Tag with the new provided name already exists.</returns>
        public async Task<TagModel> UpdateTag(int id, string name)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);

            if (await _context.Tags.AnyAsync(t => t.Name.ToLower() == name.ToLower() && t.Id != id))
            {
                throw new ArgumentException($"Tag with name {name} already exists.");
            }

            if(name != null && name != string.Empty)
                tag.Name = name;
            tag.ModifiedOn = DateTime.UtcNow;

            var tagDTO = TagMapper.MapModelFromEntity(tag);
            await _context.SaveChangesAsync();

            return tagDTO;
        }

        /// <summary>
        /// Soft-deletes a tag by setting its IsDeleted property to True.
        /// </summary>
        /// <param name="id">The ID of the target Tag.</param>
        /// <returns></returns>
        public async Task DeleteTag(int id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            ValidateTagExists(tag);
            if(tag.IsDeleted)
            {
                throw new ArgumentException("Unable to delete tag.");
            }

            tag.IsDeleted = true;
            tag.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if a Tag exists. Throws ArgumentNullException if the Tag is Null.
        /// </summary>
        /// <param name="tag">The target Tag.</param>
        public void ValidateTagExists(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("No Tag found.");
        }

        /// <summary>
        /// Sorts a collection of Tags by a given property.
        /// </summary>
        /// <param name="sort">The property to sort by.</param>
        /// <param name="tagModels">The collection of Tags.</param>
        /// <returns>Tag Model</returns>
        private ICollection<TagModel> SortTags(string sort, ICollection<TagModel> tagModels)
        {
            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "name":
                        tagModels = tagModels.OrderBy(t => t.Name).ToList();
                        break;
                    case "name_desc":
                        tagModels = tagModels.OrderByDescending(t => t.Name).ToList();
                        break;
                    case "newest":
                        tagModels = tagModels.OrderByDescending(t => t.CreatedOn).ToList();
                        break;
                    case "oldest":
                        tagModels = tagModels.OrderBy(t => t.CreatedOn).ToList();
                        break;
                    case "reports":
                        tagModels = tagModels.OrderByDescending(t => t.ReportsCount).ToList();
                        break;
                    case "reports_desc":
                        tagModels = tagModels.OrderBy(t => t.ReportsCount).ToList();
                        break;
                    default:
                        break;
                }
            }
            return tagModels;
        }

        /// <summary>
        /// Searches a collection of Tags by the Tag name.
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="tagModels">The collection of Tags.</param>
        /// <returns>ICollection of Tag Models.</returns>
        private ICollection<TagModel> SearchTags(string search, ICollection<TagModel> tagModels)
        {
            if (search != null)
            {
                tagModels = tagModels.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return tagModels;
        }
    }
}
