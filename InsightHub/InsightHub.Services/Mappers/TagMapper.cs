using InsightHub.Data.Entities;
using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class TagMapper
    {
        public static TagModel MapModelFromInput(string name)
        {
            return new TagModel()
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
            };
        }

        public static TagModel MapModelFromEntity(Tag tag)
        {
            return new TagModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                CreatedOn = tag.CreatedOn,
                ModifiedOn = tag.ModifiedOn,
                IsDeleted = tag.IsDeleted,
                DeletedOn = tag.DeletedOn,
                Reports = tag.Reports.Select(r => new string("Id: " + r.Report.Id + " - " + r.Report.Title)).ToList(),
            };
        }

        public static Tag MapEntityFromModel(TagModel tagModel)
        {
            return new Tag()
            {
                Name = tagModel.Name,
                CreatedOn = tagModel.CreatedOn
            };
        }
    }
}
