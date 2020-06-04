using InsightHub.Data.Entities;
using InsightHub.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsightHub.Services.Mappers
{
    /// <summary>
    /// Maps Reports between Models, Entities and User Input
    /// </summary>
    public static class ReportMapper
    {
        /// <summary>
        /// Creates a Report Model from User Input.
        /// </summary>
        /// <param name="title">The Report Title</param>
        /// <param name="summary">The Report Summary</param>
        /// <param name="description">The Report Description</param>
        /// <param name="imgUrl">The Image to be displayed in the Report card</param>
        /// <param name="author">The Author of the Report</param>
        /// <param name="industry">The Industry that the Report belongs to</param>
        /// <param name="tags">The Tags listed with the Report</param>
        /// <returns>Report Model</returns>
        public static ReportModel MapModelFromInput(string title, string summary, string description, string imgUrl, string author, string industry, string tags)
        {
            return new ReportModel
            {
                Title = title,
                Description = description,
                Summary = summary,
                ImgUrl = imgUrl,
                Author = author,
                Industry = industry,
                Tags = tags,
        };
        }
        /// <summary>
        /// Creates a Report Model from an existing Entity.
        /// </summary>
        /// <param name="report">The target Report Entity</param>
        /// <returns>Report</returns>
        public static ReportModel MapModelFromEntity(Report report)
        {
            return new ReportModel
            {
                Id = report.Id,
                Title = report.Title,
                Author = $"{report.Author.FirstName} {report.Author.LastName} - {report.Author.Email}",
                Summary = report.Summary,
                Description = report.Description,
                ImgUrl = report.ImgUrl,
                CreatedOn = report.CreatedOn,
                Industry = report.Industry.Name,
                DownloadsCount = report.Downloads.Count,
                IsDeleted = report.IsDeleted,
                DeletedOn = report.DeletedOn,
                IsFeatured = report.IsFeatured,
                ModifiedOn = report.ModifiedOn,
                IsPending = report.IsPending,
                Tags = string.Join(", ", report.ReportTags.Select(t => t.Tag.Name))
            };
        }
    }
}
