using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class IndustryServices : IIndustryServices
    {
        private readonly InsightHubContext _context;

        public IndustryServices(InsightHubContext context)
        {
            this._context = context ?? throw new ArgumentNullException("Context can NOT be null.");
        }

        /// <summary>
        /// Creates a new Industry in the context.
        /// </summary>
        /// <param name="name">The Name of the new Industry.</param>
        /// <param name="imgUrl">Link to the Image to be displayed in the new Industry's page.</param>
        /// <returns>IndustryModel on success. Throws ArgumentException if Industry already exists.</returns>
        public async Task<IndustryModel> CreateIndustry(string name, string imgUrl)
        {
            if (name == null)
                throw new ArgumentNullException("Name can NOT be null.");
            var dto = IndustryMapper.MapModelFromInput(name, imgUrl);
            if (await _context.Industries.AnyAsync(i => i.Name == name))
            {
                throw new ArgumentException($"Industry with name {name} already exists.");
            }
            var industry = IndustryMapper.MapEntityFromModel(dto);
            await _context.Industries.AddAsync(industry);
            await _context.SaveChangesAsync();
            dto = IndustryMapper.MapModelFromEntity(industry);
            return dto;
        }

        /// <summary>
        /// Gets an Industry by its ID.
        /// </summary>
        /// <param name="id">The ID of the target Industry.</param>
        /// <returns>IndustryModel on success. Throws ArgumentNullException if Industry doesn't exist.</returns>
        public async Task<IndustryModel> GetIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.SubscribedUsers)
                .ThenInclude(ui => ui.User)
                .Include(i => i.Reports)
                .ThenInclude(r => r.Author)
                .FirstOrDefaultAsync(i => i.Id == id);

            ValidateIndustryExists(industry);

            var dto = IndustryMapper.MapModelFromEntity(industry);
            return dto;
        }

        /// <summary>
        /// Lists all active Industries. Industry must not be soft-deleted.
        /// </summary>
        /// <param name="sort">The property to sort the list by. Can be left blank for default sort by ID.</param>
        /// <param name="search">Searches Industries by name.</param>
        /// <returns>ICollection of Industry Models</returns>
        public async Task<ICollection<IndustryModel>> GetAllIndustries(string sort, string search)
        {
            var industries = await _context.Industries
                .Where(i => !i.IsDeleted)
                .Include(i => i.SubscribedUsers)
                .ThenInclude(ui => ui.User)
                .Include(i => i.Reports)
                .ThenInclude(r => r.Author)
                .Select(i => IndustryMapper.MapModelFromEntity(i))
                .ToListAsync();

            industries = SortIndustries(sort, industries).ToList();
            industries = SearchIndustries(search, industries).ToList();

            return industries;
        }

        /// <summary>
        /// Lists all soft-deleted Industries from the context.
        /// </summary>
        /// <param name="search">Searches the Industries by name.</param>
        /// <returns>ICollection of Industry Models.</returns>
        public async Task<ICollection<IndustryModel>> GetDeletedIndustries(string search)
        {
            var industries = await _context.Industries
                .Where(i => i.IsDeleted)
                .Include(i => i.Reports)
                .Select(i => IndustryMapper.MapModelFromEntity(i))
                .ToListAsync();

            industries = SearchIndustries(search, industries).ToList();

            return industries;
        }

        /// <summary>
        /// Soft-deletes an Industry by setting its IsDeleted property to True.
        /// </summary>
        /// <param name="id">The ID of the target Industry.</param>
        /// <returns>Throws ArgumentNullException if Industry does not exist. Throws ArgumentException if Industry is already soft-deleted.</returns>
        public async Task DeleteIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Reports)
                .FirstOrDefaultAsync(i => i.Id == id);
            ValidateIndustryExists(industry);
            if (industry.IsDeleted == true)
                throw new ArgumentException("Unable to delete Industry.");
            industry.IsDeleted = true;
            industry.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the properties of an Industry.
        /// </summary>
        /// <param name="id">The ID of the target Industry.</param>
        /// <param name="name">The new Industry name.</param>
        /// <param name="imgUrl">The new Industry image path.</param>
        /// <returns></returns>
        public async Task<IndustryModel> UpdateIndustry(int id, string name, string imgUrl)
        {
            if (await _context.Industries.AnyAsync(i => i.Name == name && i.Id != id))
            {
                throw new ArgumentException($"Industry with name {name} already exists.");
            }
            var industry = await _context.Industries
                .Include(i => i.Reports)
                .Include(i => i.Reports)
                .ThenInclude(r => r.Author)
                .FirstOrDefaultAsync(i => i.Id == id);
            ValidateIndustryExists(industry);
            if(name != null && name != string.Empty)
                industry.Name = name;
            if(imgUrl != null && imgUrl != string.Empty)
                industry.ImgUrl = imgUrl;
            industry.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            var dto = IndustryMapper.MapModelFromEntity(industry);
            return dto;
        }

        /// <summary>
        /// Checks if an Industry exists in the context. Throws ArgumentNullException if it does not.
        /// </summary>
        /// <param name="industry">The target Industry.</param>
        private void ValidateIndustryExists(Industry industry)
        {
            if (industry == null)
                throw new ArgumentNullException("No Industry found.");
        }

        /// <summary>
        /// Creates a new IndustrySubscription in the context.
        /// </summary>
        /// <param name="userId">The ID of the subscribed User.</param>
        /// <param name="industryId">The ID of the target Industry.</param>
        public async Task AddSubscription(int userId, int industryId)
        {
            if (!await SubscriptionExists(userId, industryId))
            {
                await _context.IndustrySubscriptions.AddAsync(new IndustrySubscription
                {
                    UserId = userId,
                    IndustryId = industryId
                });
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an IndustrySubscription from the context.
        /// </summary>
        /// <param name="userId">The ID of the subscribed user.</param>
        /// <param name="industryId">The ID of the target industry.</param>
        /// <returns></returns>
        public async Task RemoveSubscription(int userId, int industryId)
        {
            if (await SubscriptionExists(userId, industryId))
            {
                _context.IndustrySubscriptions.Remove(new IndustrySubscription
                {
                    UserId = userId,
                    IndustryId = industryId
                });
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if an IndustrySubscription exists in the context.
        /// </summary>
        /// <param name="userId">The ID of the subscribed user.</param>
        /// <param name="industryId">The ID of the target industry.</param>
        /// <returns></returns>
        public async Task<bool> SubscriptionExists(int userId, int industryId)
        {
            return await _context.IndustrySubscriptions.AnyAsync(ui => ui.UserId == userId && ui.IndustryId == industryId);
        }

        /// <summary>
        /// Sorts a collection of industries by a given property.
        /// </summary>
        /// <param name="sort">The property to sort by.</param>
        /// <param name="industries">The target collection of industries.</param>
        /// <returns>ICollection of Industry Models.</returns>
        private ICollection<IndustryModel> SortIndustries(string sort, ICollection<IndustryModel> industries)
        {
            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "name":
                        industries = industries.OrderBy(i => i.Name).ToList();
                        break;
                    case "name_desc":
                        industries = industries.OrderByDescending(i => i.Name).ToList();
                        break;
                    case "newest":
                        industries = industries.OrderByDescending(i => i.CreatedOn).ToList();
                        break;
                    case "oldest":
                        industries = industries.OrderBy(i => i.CreatedOn).ToList();
                        break;
                    case "subscribers":
                        industries = industries.OrderByDescending(i => i.SubscriptionsCount).ToList();
                        break;
                    case "subscribers_asc":
                        industries = industries.OrderBy(i => i.SubscriptionsCount).ToList();
                        break;
                    case "reports":
                        industries = industries.OrderByDescending(i => i.ReportsCount).ToList();
                        break;
                    case "reports_asc":
                        industries = industries.OrderBy(i => i.ReportsCount).ToList();
                        break;
                    default:
                        break;
                }
            }
            return industries;
        }

        /// <summary>
        /// Searches a collection of Industries by their name.
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="industries">The target collection of Industries.</param>
        /// <returns>ICollection of Industry Models.</returns>
        private ICollection<IndustryModel> SearchIndustries(string search, ICollection<IndustryModel> industries)
        {
            if (search != null)
            {
                industries = industries.Where(i => i.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return industries;
        }

        
    }
}
