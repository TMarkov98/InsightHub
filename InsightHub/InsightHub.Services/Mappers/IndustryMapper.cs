using InsightHub.Models;
using InsightHub.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class IndustryMapper
    {
        public static IndustryDTO MapDTOFromString(string name)
        {
            return new IndustryDTO
            {
                Name = name,
                CreatedOn = DateTime.UtcNow
            };
        }
    //TODO CreatedOn??
        public static Industry MapIndusryFromDTO(IndustryDTO iDTO)
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
            };
        }
    }
}
