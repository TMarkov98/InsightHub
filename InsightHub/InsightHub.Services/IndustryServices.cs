using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IndustryModel> CreateIndustry(string name, string imgUrl)
        {
            if (name == null)
                throw new ArgumentNullException("Name can NOT be null.");
            var dto = IndustryMapper.MapModelFromInput(name, imgUrl);
            if (!await _context.Industries.AnyAsync(i => i.Name == name))
            {
                var industry = IndustryMapper.MapEntityFromModel(dto);
                await _context.Industries.AddAsync(industry);
                await _context.SaveChangesAsync();
                dto = IndustryMapper.MapModelFromEntity(industry);
                return dto;
            }
            else
            {
                throw new ArgumentException($"Industry with name {name} already exists.");
            }
        }
        public async Task<IndustryModel> GetIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Subscriptions)
                .Include(i => i.Reports)
                .ThenInclude(r => r.Author)
                .FirstOrDefaultAsync(i => i.Id == id);

            ValidateIndustryExists(industry);

            var dto = IndustryMapper.MapModelFromEntity(industry);
            return dto;
        }
        public async Task<List<IndustryModel>> GetAllIndustries(string sort, string search)
        {
            var industries = await _context.Industries
                .Where(i => !i.IsDeleted)
                .Include(i => i.Reports)
                .ThenInclude(r => r.Author)
                .Select(i => IndustryMapper.MapModelFromEntity(i))
                .ToListAsync();

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
                    default:
                        break;
                }
            }
            if (search != null)
            {
                industries = industries.Where(i => i.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return industries;
        }

        public async Task<List<IndustryModel>> GetDeletedIndustries(string search)
        {
            var industries = await _context.Industries
                .Where(i => i.IsDeleted)
                .Include(i => i.Reports)
                .Select(i => IndustryMapper.MapModelFromEntity(i))
                .ToListAsync();
            if (search != null)
            {
                industries = industries.Where(i => i.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return industries;
        }

        public async Task<bool> DeleteIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Reports)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (industry == null || industry.IsDeleted == true)
                return false;
            industry.IsDeleted = true;
            industry.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IndustryModel> UpdateIndustry(int id, string newName, string newImg)
        {
            if (await _context.Industries.AnyAsync(i => i.Name == newName && i.Id != id))
            {
                throw new ArgumentException($"Industry with name {newName} already exists.");
            }
            var industry = await _context.Industries
                .Include(i => i.Reports)
                .FirstOrDefaultAsync(i => i.Id == id);
            ValidateIndustryExists(industry);
            industry.Name = newName;
            industry.ImgUrl = newImg;
            industry.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            var dto = IndustryMapper.MapModelFromEntity(industry);
            return dto;
        }
        private void ValidateIndustryExists(Industry industry)
        {
            if (industry == null)
                throw new ArgumentNullException("No Industry found.");
        }

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

        public async Task<bool> SubscriptionExists(int userId, int industryId)
        {
            return await _context.IndustrySubscriptions.AnyAsync(ui => ui.UserId == userId && ui.IndustryId == industryId);
        }
    }
}
