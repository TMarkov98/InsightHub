﻿using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.DTOs;
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

        public async Task<IndustryDTO> CreateIndustry(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name can NOT be null.");
            var dto = IndustryMapper.MapDTOFromInput(name);
            if (!await _context.Industries.AnyAsync(i => i.Name == name))
            {
                var industry = IndustryMapper.MapIndustryFromDTO(dto);
                _context.Industries.Add(industry);
                await _context.SaveChangesAsync();
                dto = IndustryMapper.MapDTOFromIndustry(industry);
                return dto;
            }
            else
            {
                throw new ArgumentException($"Industry with name {name} already exists.");
            }
        }
        public async Task<IndustryDTO> GetIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Reports)
                .FirstOrDefaultAsync(i => i.Id == id);
            ValidateIndustryExists(industry);
            var dto = IndustryMapper.MapDTOFromIndustry(industry);
            return dto;
        }
        public async Task<List<IndustryDTO>> GetAllIndustries()
        {
            var industries = await _context.Industries
                .Where(i => !i.IsDeleted)
                .Include(i => i.Reports)
                .Select(i => IndustryMapper.MapDTOFromIndustry(i))
                .ToListAsync();
            return industries;
        }

        public async Task<List<IndustryDTO>> GetDeletedIndustries()
        {
            var industries = await _context.Industries
                .Where(i => i.IsDeleted)
                .Include(i => i.Reports)
                .Select(i => IndustryMapper.MapDTOFromIndustry(i))
                .ToListAsync();
            return industries;
        }

        public async Task<bool> DeleteIndustry(int id)
        {
            var industry = await _context.Industries
                .Include(i => i.Reports)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (industry.IsDeleted == true || industry == null)
                return false;
            industry.IsDeleted = true;
            industry.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IndustryDTO> UpdateIndustry(int id, string newName)
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
            industry.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            var dto = IndustryMapper.MapDTOFromIndustry(industry);
            return dto;
        }
        private void ValidateIndustryExists(Industry industry)
        {
            if (industry == null)
                throw new ArgumentNullException("No Industry found.");
        }
    }
}
