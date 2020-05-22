using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class UserServices : IUserServices
    {
        private readonly InsightHubContext _insightHubContext;

        public UserServices(InsightHubContext insightHubContext)
        {
            _insightHubContext = insightHubContext;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var user = await _insightHubContext.Users
                .Include(u => u.Reports)
                .Include(u => u.Role)
                .Include(u => u.IndustrySubscriptions)
                .Include(u => u.Reports)
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new ArgumentNullException("User not found.");
            var userDTO = UserMapper.MapModelFromEntity(user);
            return userDTO;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _insightHubContext.Users
                .Where(u => !u.IsBanned && !u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<List<UserModel>> GetBannedUsers()
        {
            var users = await _insightHubContext.Users
                .Where(u => u.IsBanned)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<List<UserModel>> GetPendingUsers()
        {
            var users = await _insightHubContext.Users
                .Where(u => u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool isBanned, string banReason)
        {
            var user = await _insightHubContext.Users
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsBanned)
                ?? throw new ArgumentNullException("User not found.");

            user.FirstName = firstName;
            user.LastName = lastName;
            user.LockoutEnabled = isBanned;
            user.BanReason = banReason;

            var userDTO = UserMapper.MapModelFromEntity(user);
            _insightHubContext.SaveChanges();
            return userDTO;
        }

        public async Task BanUser(int id, string reason)
        {
            var user = await _insightHubContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || user.LockoutEnabled)
                throw new ArgumentException("Unable to ban user.");

            user.IsBanned = true;
            user.BanReason = reason;
            user.LockoutEnd = DateTime.Parse("2555-01-01 00:00:00.00");
            _insightHubContext.SaveChanges();
        }

        public async Task UnbanUser(int id)
        {
            var user = await _insightHubContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || !user.LockoutEnabled)
                throw new ArgumentException("Unable to unban user.");

            user.IsBanned = false;
            user.BanReason = string.Empty;
            user.LockoutEnd = DateTime.Parse("2000-01-01 00:00:00.00");
            _insightHubContext.SaveChanges();
        }

        public async Task<List<ReportModel>> GetDownloadedReports(int userId)
        {
            var reports = await _insightHubContext.DownloadedReports
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Author)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Industry)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Tags)
                .ThenInclude(r => r.Tag)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Downloads)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ReportMapper.MapModelFromEntity(ur.Report))
                .ToListAsync();

            return reports;
        }

        public async Task<List<ReportModel>> GetMyReports(int userId)
        {
            var reports = await _insightHubContext.Reports
                .Include(r => r.Author)
                .Include(r => r.Industry)
                .Include(r => r.Tags)
                .ThenInclude(r => r.Tag)
                .Include(r => r.Downloads)
                .Where(r => r.AuthorId == userId)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            return reports;
        }
    }
}
