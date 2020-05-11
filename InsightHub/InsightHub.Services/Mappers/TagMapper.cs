using InsightHub.Models;
using InsightHub.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class TagMapper
    {
        public static TagDTO MapDTOFromInput(string name)
        {
            return new TagDTO()
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
            };
        }

        public static TagDTO MapDTOFromTag(Tag tag)
        {
            return new TagDTO()
            {
                Id = tag.Id,
                Name = tag.Name,
                CreatedOn = tag.CreatedOn,
                ModifiedOn = tag.ModifiedOn,
                IsDeleted = tag.IsDeleted,
                Reports = tag.Reports.Select(r => new string("Id: " + r.Report.Id + " - " + r.Report.Title)).ToList(),
            };
        }

        public static Tag MapTagFromDTO(TagDTO tagDTO)
        {
            return new Tag()
            {
                Name = tagDTO.Name,
                CreatedOn = tagDTO.CreatedOn
            };
        }
    }
}
