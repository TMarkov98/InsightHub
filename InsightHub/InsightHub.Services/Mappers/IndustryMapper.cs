using InsightHub.Data.Entities;
using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    /// <summary>
    /// Maps Industries between Models, Entities and User Input
    /// </summary>
    public static class IndustryMapper
    {
        /// <summary>
        /// Creates a new Industry Model from User Input
        /// </summary>
        /// <param name="name">The name of the Industry</param>
        /// <param name="url">The Image URL to be displayed with the Industry</param>
        /// <returns>Industry Model</returns>
        public static IndustryModel MapModelFromInput(string name, string url)
        {
            return new IndustryModel
            {
                Name = name,
                ImgUrl = url ?? "https://i.imgur.com/AoW6Iqh.png",
                CreatedOn = DateTime.UtcNow
            };
        }
        /// <summary>
        /// Creates a new Industry Entity from a Model
        /// </summary>
        /// <param name="industryModel">The target Industry Model</param>
        /// <returns>Industry</returns>
        public static Industry MapEntityFromModel(IndustryModel industryModel)
        {
            return new Industry
            {
                Name = industryModel.Name,
                ImgUrl = industryModel.ImgUrl,
                CreatedOn = industryModel.CreatedOn,
            };
        }
        /// <summary>
        /// Maps and Industry Model from an existing Entity
        /// </summary>
        /// <param name="industry">The target Industry Entity</param>
        /// <returns>Industry Model</returns>
        public static IndustryModel MapModelFromEntity(Industry industry)
        {
            return new IndustryModel
            {
                Id = industry.Id,
                Name = industry.Name,
                ImgUrl = industry.ImgUrl,
                CreatedOn = industry.CreatedOn,
                ModifiedOn = industry.ModifiedOn,
                IsDeleted = industry.IsDeleted,
                DeletedOn = industry.DeletedOn,
                Reports = industry.Reports.Select(r => ReportMapper.MapModelFromEntity(r)).ToList(),
                ReportsCount = industry.Reports.Count(),
                SubscriptionsCount = industry.SubscribedUsers.Count,
                SubscribedUsers = industry.SubscribedUsers.Select(ui => ui.User.Email)
            };
        }
    }
}
