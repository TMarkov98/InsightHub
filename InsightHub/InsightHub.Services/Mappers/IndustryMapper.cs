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
        public static IndustryModel MapModelFromInput(string name, string url)
        {
            return new IndustryModel
            {
                Name = name,
                ImgUrl = url,
                CreatedOn = DateTime.UtcNow
            };
        }
        public static Industry MapEntityFromModel(IndustryModel industryModel)
        {
            return new Industry
            {
                Name = industryModel.Name,
                ImgUrl = industryModel.ImgUrl,
                CreatedOn = industryModel.CreatedOn,
            };
        }
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
                Reports = industry.Reports.Select(r => ReportMapper.MapModelFromEntity(r) ).ToList(),
            };
        }
    }
}
