using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.DTOs;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ReportDTO> CreateReport(string title, string description, string author, string industry, string tags)
        {
            var reportDTO = ReportMapper.MapDTOFromInput(title, description, author, industry, tags);
            if (!await _context.Reports.AnyAsync(r => r.Title == title))
            {
                var report = new Report()
                {
                    Title = reportDTO.Title,
                    Description = reportDTO.Description,
                    AuthorId = _context.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == reportDTO.Author.ToUpper()).Id,
                    IndustryId = _context.Industries.FirstOrDefaultAsync(i => i.Name.ToUpper() == reportDTO.Industry.ToUpper()).Id
                };
                _context.Reports.Add(report);
                AddTagsToReport(report, reportDTO.Tags);

                await _context.SaveChangesAsync();
                reportDTO = ReportMapper.MapDTOFromReport(report);
                return reportDTO;
            }
            throw new ArgumentException($"Report with title {title} already exists.");
        }

        public async Task<ICollection<ReportDTO>> GetReports()
        {
            var reports = await _context.Reports
                .Where(r => !r.IsDeleted)
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .Select(r => ReportMapper.MapDTOFromReport(r))
                .ToListAsync();
            return reports;
        }

        public async Task<ReportDTO> GetReport(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .ThenInclude(rt => rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            var reportDTO = ReportMapper.MapDTOFromReport(report);
            return reportDTO;
        }

        public async Task<ReportDTO> UpdateReport(int id, string title, string description, string industry, string tags)
        {
            if (await _context.Reports.AnyAsync(r => r.Title == title))
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
            var reportDTO = ReportMapper.MapDTOFromInput(title, description, null, industry, tags);
            report.Title = reportDTO.Title;
            report.Description = reportDTO.Description;
            report.Industry = await _context.Industries.FirstOrDefaultAsync(i => i.Name == reportDTO.Industry);
            report.Tags.Clear();
            AddTagsToReport(report, reportDTO.Tags);
            reportDTO = ReportMapper.MapDTOFromReport(report);
            return reportDTO;
        }

        public async Task<bool> DeleteReport(int id)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.Id == id);
            if (report.IsDeleted == true || report == null)
                return false;
            report.IsDeleted = true;
            report.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReportDTO> TogglePending(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            if (report.IsPending)
            {
                report.IsPending = false;
            }
            else
            {
                report.IsPending = true;
            }
            var reportDTO = ReportMapper.MapDTOFromReport(report);
            return reportDTO;
        }

        public async Task<ReportDTO> ToggleFeatured(int id)
        {
            var report = await _context.Reports
                .Include(r => r.Industry)
                .Include(r => r.Author)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == id);
            ValidateReportExists(report);
            if (report.IsFeatured)
            {
                report.IsFeatured = false;
            }
            else
            {
                report.IsFeatured = true;
            }
            var reportDTO = ReportMapper.MapDTOFromReport(report);
            return reportDTO;
        }

        private void AddTagsToReport(Report report, List<string> tags)
        {
            foreach (var tag in tags)
            {
                _context.ReportTags.Add(new ReportTag
                {
                    ReportId = report.Id,
                    TagId = CreateTagIfDoesntExist(tag).Id,
                });
            }
        }

        private async Task<Tag> CreateTagIfDoesntExist(string name)
        {
            if (!await _context.Tags.AnyAsync(t => t.Name == name))
            {
                await _tagServices.CreateTag(name);
            }
            var tag = _context.Tags.FirstOrDefault(t => t.Name == name);
            return tag;
        }

        private void ValidateReportExists(Report report)
        {
            if (report == null)
                throw new ArgumentNullException("No Report found.");
        }
    }
}
