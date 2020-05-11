using InsightHub.Models;
using InsightHub.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class IndustryMapper
    {
        public static IndustryDTO MapDTOFromInput(string name)
        {
            return new IndustryDTO
            {
                Name = name,
                CreatedOn = DateTime.UtcNow
            };
        }
        public static Industry MapIndustryFromDTO(IndustryDTO iDTO)
        {
            return new Industry
            {
                Name = iDTO.Name,
                CreatedOn = iDTO.CreatedOn,
            };
        }
        public static IndustryDTO MapDTOFromIndustry(Industry industry)
        {
            return new IndustryDTO
            {
                Id = industry.Id,
                Name = industry.Name,
                CreatedOn = industry.CreatedOn,
                ModifiedOn = industry.ModifiedOn,
                IsDeleted = industry.IsDeleted,
                Reports = industry.Reports.Select(r => new string("Id: " + r.Id + " - " + r.Title)).ToList(),
            };
        }
    }
}
