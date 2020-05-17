using InsightHub.Data.Entities;
using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class IndustryMapper
    {
        public static IndustryModel MapModelFromInput(string name)
        {
            return new IndustryModel
            {
                Name = name,
                CreatedOn = DateTime.UtcNow
            };
        }
        public static Industry MapEntityFromModel(IndustryModel industryModel)
        {
            return new Industry
            {
                Name = industryModel.Name,
                CreatedOn = industryModel.CreatedOn,
            };
        }
        public static IndustryModel MapModelFromEntity(Industry industry)
        {
            return new IndustryModel
            {
                Id = industry.Id,
                Name = industry.Name,
                CreatedOn = industry.CreatedOn,
                ModifiedOn = industry.ModifiedOn,
                IsDeleted = industry.IsDeleted,
                DeletedOn = industry.DeletedOn,
                Reports = industry.Reports.Select(r => $"Id: {r.Id} - {r.Title}").ToList(),
            };
        }
    }
}
