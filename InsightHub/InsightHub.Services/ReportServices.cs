using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Services.DTOs;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class ReportServices
    {
        private readonly InsightHubContext _context;
        private readonly TagServices _tagServices;

        public ReportServices(InsightHubContext context)
        {
            this._context = context ?? throw new ArgumentNullException("Context can NOT be null.");
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
                foreach (var tag in reportDTO.Tags)
                {
                    _context.ReportTags.Add(new ReportTag
                    {
                        ReportId = report.Id,
                        TagId = CreateTagIfDoesntExist(tag).Id,
                    });
                }
                await _context.SaveChangesAsync();
                reportDTO = ReportMapper.MapDTOFromReport(report);
                return reportDTO;
            }
            throw new ArgumentException($"Report with title {title} already exists.");
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
    }
}
