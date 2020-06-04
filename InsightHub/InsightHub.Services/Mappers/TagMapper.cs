using InsightHub.Data.Entities;
using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    /// <summary>
    /// Maps Tags between Models, Entities and User Input
    /// </summary>
    public static class TagMapper
    {
        /// <summary>
        /// Creates a Tag Model from User Input
        /// </summary>
        /// <param name="name">The Tag Name</param>
        /// <returns>Tag Model</returns>
        public static TagModel MapModelFromInput(string name)
        {
            return new TagModel()
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
            };
        }
        /// <summary>
        /// Maps a Tag Model from an existing Entity
        /// </summary>
        /// <param name="tag">The target Tag Entity</param>
        /// <returns>Tag Model</returns>
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
                Reports = tag.ReportTags.Select(r => $"Id: {r.Report.Id} - {r.Report.Title}").ToList(),
                ReportsCount = tag.ReportTags.Count,
            };
        }
        /// <summary>
        /// Maps a Tag Entity from an existing Model
        /// </summary>
        /// <param name="tagModel">The target Tag Model</param>
        /// <returns>Tag</returns>
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
