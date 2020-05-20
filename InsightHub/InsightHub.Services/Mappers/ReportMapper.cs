using InsightHub.Data.Entities;
using InsightHub.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class ReportMapper
    {
        public static ReportModel MapModelFromInput(string title, string description,string imgUrl, string author, string industry, string tags)
        {
            return new ReportModel
            {
                Title = title,
                Description = description,
                ImgUrl = imgUrl,
                Author = author,
                Industry = industry,
                Tags = tags,
        };
        }
        public static ReportModel MapModelFromEntity(Report report)
        {
            return new ReportModel
            {
                Id = report.Id,
                Title = report.Title,
                Author = $"{report.Author.FirstName} {report.Author.LastName} - {report.Author.Email}",
                Description = report.Description,
                ImgUrl = report.ImgUrl,
                CreatedOn = report.CreatedOn,
                Industry = report.Industry.Name,
                DownloadsCount = report.Downloads.Count,
                IsDeleted = report.IsDeleted,
                DeletedOn = report.DeletedOn,
                IsFeatured = report.IsFeatured,
                ModifiedOn = report.ModifiedOn,
                Tags = string.Join(", ", report.Tags.Select(t => t.Tag.Name))
            };
        }
    }
}
