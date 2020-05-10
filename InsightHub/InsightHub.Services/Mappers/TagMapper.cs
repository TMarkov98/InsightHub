using InsightHub.Models;
using InsightHub.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class TagMapper
    {
        public static TagDTO MapDTOFromString(string name)
        {
            return new TagDTO()
            {
                Name = name,
                CreatedOn = DateTime.Now,
            };
        }

        public static TagDTO MapDTOFromTag(Tag tag)
        {
            return new TagDTO()
            {
                Id = tag.Id,
                Name = tag.Name,
                CreatedOn = tag.CreatedOn,
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
