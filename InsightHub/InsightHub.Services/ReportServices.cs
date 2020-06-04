using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class ReportServices : IReportServices
    {
        private readonly InsightHubContext _context;
        private readonly ITagServices _tagServices;

        public ReportServices(InsightHubContext context, ITagServices tagServices)
        {
            this._context = context ?? throw new ArgumentNullException("Context can NOT be null.");
            this._tagServices = tagServices ?? throw new ArgumentNullException("Tag Services can NOT be null.");
        }

        /// <summary>
        /// Creates a new Report from given Strings and places it in Pending.
        /// </summary>
        /// <param name="title">The title of the new Report. Between 5 and 100 characters.</param>
        /// <param name="summary">The summary of the new Report. Between 5 and 300 characters.</param>
        /// <param name="description">The description of the new Report. Between 5 and 5000 characters.</param>
        /// <param name="author">The author of the new Report. Automatically generated from Identity and the logged in User.</param>
        /// <param name="imgUrl">The URL for the report's image, which appears on the report's card.</param>
        /// <param name="industry">The Industry under which the new Report will be classified. Has to match an existing Industry in the context.</param>
        /// <param name="tags">The Tags to be added to the new Report. If a tag does not exist, it will be created automatically.</param>
        /// <returns>On success - A Report Model, mapped from the new Report. If the Report already exists - Throws Argument Exception</returns>
        public async Task<ReportModel> CreateReport(string title, string summary, string description, string author, string imgUrl, string industry, string tags)
        {
            var reportModel = ReportMapper.MapModelFromInput(title, summary, description, imgUrl, author, industry, tags);

            // Throw if Report with this title exists.
            if (await _context.Reports
                .Include(r => r.Author)
                .Include(r => r.Industry)
                .Include(r => r.ReportTags)
                .AnyAsync(r => r.Title == title))
            {

                throw new ArgumentException($"Report with title {title} already exists.");
            }

            //Create Report
            var report = new Report()
            {
                Title = reportModel.Title,
                Description = reportModel.Description,
                Summary = reportModel.Summary,
                CreatedOn = DateTime.UtcNow
            };
            await _context.Reports.AddAsync(report);

            //Map Author, Industry, set Pending
            report.AuthorId = _context.Users.FirstOrDefault(u => u.NormalizedEmail == reportModel.Author.ToUpper()).Id;
            report.IndustryId = _context.Industries.FirstOrDefault(i => i.Name.ToUpper() == reportModel.Industry.ToUpper()).Id;
            report.IsPending = true;
            await _context.SaveChangesAsync();

            //Map Tags, Create new Tags if any do not exist.
            await AddTagsToReport(report, reportModel.Tags);
            await _context.SaveChangesAsync();

            //Return Report Model
            reportModel = ReportMapper.MapModelFromEntity(report);
            return reportModel;
        }

        /// <summary>
        /// Lists Active Reports from context. Each Report must not be Pending or Deleted. Each Report's Industry must not be Deleted.
        /// /// </summary>
        /// <param name="sort">The property to sort the Reports by. Can be left null for default sorting by ID.</param>
        /// <param name="search">Searches the Report List by Name and Summary. Can be left null to display all Reports.</param>
        /// <param name="author">Filters the Report List by matching the author's Email address.</param>
        /// <param name="industry">Filters the Report List by Industry.</param>
        /// <param name="tag">Filters the Report List by Tag.</param>
        /// <returns>ICollection of Report Models</returns>
        public async Task<ICollection<ReportModel>> GetReports(string sort, string search, string author, string industry, string tag)
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Include(r => r.Industry)
                .Where(r => !r.Industry.IsDeleted)
                .Include(r => r.Author)
                .Include(r => r.Downloads)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            //Sort Reports
            reports = SortReports(sort, reports).ToList();

            //Search Reports
            reports = SearchReports(search, reports).ToList();

            //Filter Reports
            if (author != null)
            {
                reports = reports.Where(r => r.Author.ToLower().Contains(author.ToLower())).ToList();
            }
            if (industry != null)
            {
                reports = reports.Where(r => r.Industry.ToLower().Contains(industry.ToLower())).ToList();
            }
            if (tag != null)
            {
                reports = reports.Where(r => string.Join(' ', r.Tags).ToLower().Contains(tag.ToLower())).ToList();
            }
            return reports;
        }

        /// <summary>
        /// Lists all Featured Reports. Reports must not be Pending or Deleted. Reports' Industry must not be Deleted.
        /// </summary>
        /// <returns>ICollection of Report Models</returns>
        public async Task<ICollection<ReportModel>> GetFeaturedReports()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Where(r => r.IsFeatured)
                .Include(r => r.Industry)
                .Where(r => !r.Industry.IsDeleted)
                .Include(r => r.Downloads)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .OrderByDescending(r => r.ModifiedOn)
                .Take(4)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            return reports;
        }

        /// <summary>
        /// Lists all Deleted Reports.
        /// </summary>
        /// <param name="sort">The property to sort the Reports by. Can be left null for default sorting by ID.</param>
        /// <param name="search">Searches the Report List by Name and Summary. Can be left null to display all Reports.</param>
        /// <returns>ICollection of Report Models</returns>
        public async Task<ICollection<ReportModel>> GetDeletedReports(string sort, string search)
        {
            var reports = await _context.Reports
                .Where(r => r.IsDeleted)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            reports = SortReports(sort, reports).ToList();

            reports = SearchReports(search, reports).ToList();

            return reports;
        }

        /// <summary>
        /// Lists all Pending Reports
        /// </summary>
        /// <param name="sort">The property to sort the Reports by. Can be left null for default sorting by ID.</param>
        /// <param name="search">Searches the Report List by Name and Summary. Can be left null to display all Reports.</param>
        /// <returns>ICollection of Report Models</returns>
        public async Task<ICollection<ReportModel>> GetPendingReports(string sort, string search)
        {
            var reports = await _context.Reports
                .Where(r => r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            reports = SortReports(sort, reports).ToList();

            reports = SearchReports(search, reports).ToList();

            return reports;
        }

        /// <summary>
        /// Lists the 4 Newest Reports in the context.
        /// </summary>
        /// <returns>ICollection of Report Models</returns>
        public async Task<ICollection<ReportModel>> GetNewestReports()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Downloads)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .OrderByDescending(r => r.CreatedOn)
                .Take(4)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();
            return reports;
        }

        /// <summary>
        /// Lists the 4 Most Downloaded Reports in the context.
        /// </summary>
        /// <returns>ICollection of Report Models</returns>
        public async Task<ICollection<ReportModel>> GetMostDownloadedReports()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Downloads)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .OrderByDescending(r => r.Downloads.Count)
                .Take(4)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();
            return reports;
        }

        /// <summary>
        /// Gets a report from the context by its ID.
        /// </summary>
        /// <param name="id">The ID for the target Report.</param>
        /// <returns>On success - a Report Model. Throws ArgumentNullException if ID is invalid.</returns>
        public async Task<ReportModel> GetReport(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            var reportDTO = ReportMapper.MapModelFromEntity(report);
            return reportDTO;
        }

        /// <summary>
        /// Updates the properties of an existing Report, then moves it to the Pending list.
        /// </summary>
        /// <param name="id">The ID for the target Report.</param>
        /// <param name="title">The new Title of the Report.</param>
        /// <param name="summary">The new Summary of the Report.</param>
        /// <param name="description">The new Description of the Report.</param>
        /// <param name="imgUrl">The path to the new Image to be displayed for the Report.</param>
        /// <param name="industry">The new Industry of the Report.</param>
        /// <param name="tags">The new list of Tags for the Report. New Tags will be created if any do not exist.</param>
        /// <returns>A Report Model on success. </returns>
        public async Task<ReportModel> UpdateReport(int id, string title, string summary, string description, string imgUrl, string industry, string tags)
        {
            //Throw if report with new Title already exists.
            if (await _context.Reports.AnyAsync(r => r.Title == title && r.Id != id))
            {
                throw new ArgumentException($"Report with title {title} already exists.");
            }
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);

            //Throw if ID is invalid.
            ValidateReportExists(report);


            var reportModel = ReportMapper.MapModelFromInput(title, summary, description, imgUrl, null, industry, tags);
            //Map Title
            if(title != null && title != string.Empty)
                report.Title = reportModel.Title;
            //Map Summary
            if (summary != null && summary != string.Empty)
                report.Summary = reportModel.Summary;
            //Map Description
            if(description != null && description != string.Empty)
                report.Description = reportModel.Description;
            //Map Image
            if (imgUrl != null && imgUrl != string.Empty)
                report.ImgUrl = reportModel.ImgUrl;
            //Map Industry
            report.Industry = await _context.Industries.FirstOrDefaultAsync(i => i.Name == reportModel.Industry);
            //Set ModifiedOn
            report.ModifiedOn = DateTime.UtcNow;
            report.ReportTags.Clear();
            //Set Pending
            report.IsPending = true;
            await _context.SaveChangesAsync();
            //Map Tags, Add new Tags to Context if tags are not found.
            await AddTagsToReport(report, reportModel.Tags);
            await _context.SaveChangesAsync();
            //Return Report Model
            reportModel = ReportMapper.MapModelFromEntity(report);
            return reportModel;
        }


        /// <summary>
        /// Soft-deletes a Report by setting the IsDeleted property to True.
        /// </summary>
        /// <param name="id">The ID of the target Report.</param>
        /// <returns>Throws ArgumentNullException if Report does not exist or ArgumentException if it is already soft-deleted.</returns>
        public async Task DeleteReport(int id)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            if (report.IsDeleted)
            {
                throw new ArgumentException("Unable to delete report.");
            }
            report.IsDeleted = true;
            report.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the IsDeleted property of a Report.
        /// </summary>
        /// <param name="id">The ID of the target Report.</param>
        /// <returns>Throws ArgumentNullException if Report does not exist or ArgumentException if it is not soft-deleted.</returns>
        public async Task RestoreReport(int id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            if (!report.IsDeleted)
            {
                throw new ArgumentException("Unable to restore report.");
            }
            report.IsDeleted = false;
            report.DeletedOn = DateTime.MinValue;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Permanently deletes a soft-deleted Report from the context.
        /// </summary>
        /// <param name="id">The ID of the target Report.</param>
        /// <returns>Throws ArgumentNullException if Report does not exist or ArgumentException if it is not soft-deleted.</returns>
        public async Task PermanentlyDeleteReport(int id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            if (!report.IsDeleted)
            {
                throw new ArgumentException("Unable to delete report.");
            }
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Approves a Pending Report.
        /// </summary>
        /// <param name="id">The ID of the target Report.</param>
        /// <returns>Throws ArgumentNullException if the Report does not exist.</returns>
        public async Task ApproveReport(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .ThenInclude(ur => ur.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);

            if (report.IsPending)
                report.IsPending = false;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Handles the Featured property of a Report.
        /// </summary>
        /// <param name="id">The ID of the target Report.</param>
        /// <returns>Throws ArgumentNullException if the Report does not exist.</returns>
        public async Task ToggleFeatured(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.ReportTags)
                .FirstOrDefaultAsync(r => r.Id == id);

            ValidateReportExists(report);

            if (report.IsFeatured)
                report.IsFeatured = false;
            else
                report.IsFeatured = true;
            report.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a new DownloadedReport in the context.
        /// </summary>
        /// <param name="userId">The ID of the User who downloads the Report.</param>
        /// <param name="reportId">The ID of the target Report.</param>
        public async Task AddToDownloadsCount(int userId, int reportId)
        {
            if (!await _context.DownloadedReports.AnyAsync(ur => ur.UserId == userId && ur.ReportId == reportId))
            {
                await _context.DownloadedReports.AddAsync(new DownloadedReport
                {
                    UserId = userId,
                    ReportId = reportId
                });
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Adds Tags to a target Report. Creates Tags if they don't exist.
        /// </summary>
        /// <param name="report">The target Report.</param>
        /// <param name="tags">String containing all the tags.</param>
        private async Task AddTagsToReport(Report report, string tags)
        {
            var tagsList = tags.Split(',', ';', '.');
            foreach (var tag in tagsList)
            {
                _context.ReportTags.Add(new ReportTag
                {
                    ReportId = report.Id,
                    TagId = (await CreateTagIfDoesntExist(tag)).Id,
                });
            }
        }

        /// <summary>
        /// Checks if a tag exists in the context. Creates a new Tag if it does not.
        /// </summary>
        /// <param name="name">The Name of the new Tag.</param>
        /// <returns>Tag</returns>
        private async Task<Tag> CreateTagIfDoesntExist(string name)
        {
            name = name.ToLower().Trim();
            if (!await _context.Tags.AnyAsync(t => t.Name.ToLower() == name))
            {
                await _tagServices.CreateTag(name);
            }
            var tag = _context.Tags.FirstOrDefault(t => t.Name.ToLower() == name);
            return tag;
        }

        /// <summary>
        /// Validates if a Report exists. Throws ArgumentNullException if the Report is Null.
        /// </summary>
        /// <param name="report">The target Report.</param>
        private void ValidateReportExists(Report report)
        {
            if (report == null)
                throw new ArgumentNullException("No Report found.");
        }

        /// <summary>
        /// Sorts a collection of Reports by a given property.
        /// </summary>
        /// <param name="sort">The property to sort the Reports by.</param>
        /// <param name="reports">The target collection of Reports.</param>
        /// <returns>ICollection of Report Models.</returns>
        private ICollection<ReportModel> SortReports(string sort, ICollection<ReportModel> reports)
        {
            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "name":
                    case "title":
                        reports = reports.OrderBy(r => r.Title).ToList();
                        break;
                    case "title_desc":
                        reports = reports.OrderByDescending(r => r.Title).ToList();
                        break;
                    case "author":
                    case "user":
                    case "creator":
                        reports = reports.OrderBy(r => r.Author).ToList();
                        break;
                    case "author_desc":
                        reports = reports.OrderByDescending(r => r.Author).ToList();
                        break;
                    case "industry":
                        reports = reports.OrderBy(r => r.Industry).ToList();
                        break;
                    case "industry_desc":
                        reports = reports.OrderByDescending(r => r.Industry).ToList();
                        break;
                    case "newest":
                        reports = reports.OrderByDescending(r => r.CreatedOn).ToList();
                        break;
                    case "oldest":
                        reports = reports.OrderBy(r => r.CreatedOn).ToList();
                        break;
                    case "downloads":
                        reports = reports.OrderByDescending(r => r.DownloadsCount).ToList();
                        break;
                    case "downloads_asc":
                        reports = reports.OrderBy(r => r.DownloadsCount).ToList();
                        break;
                    default:
                        break;
                }
            }
            return reports;
        }

        /// <summary>
        /// Searches a collection of Reports by its Title and Summary.
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="reports">The target collection of Reports.</param>
        /// <returns>ICollection of Report Models.</returns>
        private ICollection<ReportModel> SearchReports(string search, ICollection<ReportModel> reports)
        {
            if (search != null)
            {
                reports = reports.Where(r => r.Title.ToLower().Contains(search.ToLower())
                    || r.Summary.ToLower().Contains(search.ToLower())).ToList();
            }
            return reports;
        }
    }
}
