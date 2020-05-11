using InsightHub.Models;
using InsightHub.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public static class ReportMapper
    {
        public static ReportDTO MapDTOFromInput(string title, string description, string author, string industry, string tags)
        {
            return new ReportDTO
            {
                Title = title,
                Description = description,
                Author = author,
                Industry = industry,
                Tags = tags.Split(';',',').Select(t => t.Trim()).ToList(),
        };
        }
        public static ReportDTO MapDTOFromReport(Report report)
        {
            return new ReportDTO
            {
                Id = report.Id,
                Title = report.Title,
                Author = new string(report.Author.FirstName + " " + report.Author.LastName + " - " + report.Author.Email),
                Description = report.Description,
                CreatedOn = report.CreatedOn,
                Industry = report.Industry.Name,
                IsDeleted = report.IsDeleted,
                IsFeatured = report.IsFeatured,
                ModifiedOn = report.ModifiedOn,
                Tags = report.Tags.Select(t => t.Tag.Name).ToList()
            };
        }
    }
}
