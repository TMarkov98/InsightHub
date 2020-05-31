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
        public async Task<ReportModel> CreateReport(string title, string summary, string description, string author, string imgUrl, string industry, string tags)
        {
            var reportDTO = ReportMapper.MapModelFromInput(title, summary, description, imgUrl, author, industry, tags);
            if (!await _context.Reports
                .Include(r => r.Author)
                .Include(r => r.Industry)
                .Include(r => r.Tags)
                .AnyAsync(r => r.Title == title))
            {
                var report = new Report()
                {
                    Title = reportDTO.Title,
                    Description = reportDTO.Description,
                    Summary = reportDTO.Summary,
                    CreatedOn = DateTime.UtcNow
                };
                await _context.Reports.AddAsync(report);
                report.AuthorId = _context.Users.FirstOrDefault(u => u.NormalizedEmail == reportDTO.Author.ToUpper()).Id;
                report.IndustryId = _context.Industries.FirstOrDefault(i => i.Name.ToUpper() == reportDTO.Industry.ToUpper()).Id;
                report.IsPending = true;
                await _context.SaveChangesAsync();
                await AddTagsToReport(report, reportDTO.Tags);

                await _context.SaveChangesAsync();
                reportDTO = ReportMapper.MapModelFromEntity(report);
                return reportDTO;
            }
            throw new ArgumentException($"Report with title {title} already exists.");
        }

        public async Task<ICollection<ReportModel>> GetReports(string sort, string search, string author, string industry, string tag)
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Downloads)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            reports = SortReports(sort, reports).ToList();

            reports = SearchReports(search, reports).ToList();

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
        public async Task<ICollection<ReportModel>> GetReportsFeatured()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Where(r => r.IsFeatured)
                .Include(r => r.Industry)
                .Include(r => r.Downloads)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .OrderByDescending(r => r.ModifiedOn)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            return reports;
        }
        public async Task<ICollection<ReportModel>> GetReportsDeleted(string sort, string search)
        {
            var reports = await _context.Reports
                .Where(r => r.IsDeleted)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            reports = SortReports(sort, reports).ToList();

            reports = SearchReports(search, reports).ToList();

            return reports;
        }

        public async Task<ICollection<ReportModel>> GetReportsPending(string sort, string search)
        {
            var reports = await _context.Reports
                .Where(r => r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            reports = SortReports(sort, reports).ToList();

            reports = SearchReports(search, reports).ToList();

            return reports;
        }

        public async Task<ICollection<ReportModel>> GetTop5NewReports()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Downloads)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .OrderByDescending(r => r.CreatedOn)
                .Take(5)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();
            return reports;
        }
        public async Task<ICollection<ReportModel>> GetTop5MostDownloads()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Where(r => !r.IsPending)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Downloads)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .OrderByDescending(r => r.Downloads.Count)
                .Take(5)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();
            return reports;
        }
        

        public async Task<ReportModel> GetReport(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            var reportDTO = ReportMapper.MapModelFromEntity(report);
            return reportDTO;
        }

        public async Task<ReportModel> UpdateReport(int id, string title, string summary, string description, string imgUrl, string industry, string tags)
        {
            if (await _context.Reports.AnyAsync(r => r.Title == title && r.Id != id))
            {
                throw new ArgumentException($"Report with title {title} already exists.");
            }
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            var reportDTO = ReportMapper.MapModelFromInput(title, summary, description, imgUrl, null, industry, tags);
            report.Title = reportDTO.Title;
            report.Summary = reportDTO.Summary;
            report.Description = reportDTO.Description;
            report.ImgUrl = reportDTO.ImgUrl;
            report.Industry = await _context.Industries.FirstOrDefaultAsync(i => i.Name == reportDTO.Industry);
            report.ModifiedOn = DateTime.UtcNow;
            report.Tags.Clear();
            report.IsPending = true;
            await _context.SaveChangesAsync();
            await AddTagsToReport(report, reportDTO.Tags);
            await _context.SaveChangesAsync();
            reportDTO = ReportMapper.MapModelFromEntity(report);
            return reportDTO;
        }

        public async Task DeleteReport(int id)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.Id == id);
            if (report.IsDeleted == true || report == null)
                throw new ArgumentException("Unable to delete report.");
            report.IsDeleted = true;
            report.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task RestoreReport(int id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
            if (report.IsDeleted == false || report == null)
                throw new ArgumentException("Unable to restore report.");
            report.IsDeleted = false;
            report.DeletedOn = DateTime.MinValue;
            await _context.SaveChangesAsync();
        }

        public async Task PermanentlyDeleteReport(int id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
            if (report.IsDeleted == false || report == null)
                throw new ArgumentException("Unable to delete report.");
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveReport(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(ur => ur.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);

            if (report.IsPending)
                report.IsPending = false;

            await _context.SaveChangesAsync();
        }

        public async Task ToggleFeatured(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == id);

            ValidateReportExists(report);

            if (report.IsFeatured)
                report.IsFeatured = false;
            else
                report.IsFeatured = true;
            report.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task AddToDownloadsCount(int userId, int reportId)
        {
            if(!await _context.DownloadedReports.AnyAsync(ur => ur.UserId == userId && ur.ReportId == reportId))
            {
                await _context.DownloadedReports.AddAsync(new DownloadedReport
                {
                    UserId = userId,
                    ReportId = reportId
                });
                await _context.SaveChangesAsync();
            }
        }

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

        private void ValidateReportExists(Report report)
        {
            if (report == null)
                throw new ArgumentNullException("No Report found.");
        }

        private ICollection<ReportModel> SortReports(string sort, List<ReportModel> reports)
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
