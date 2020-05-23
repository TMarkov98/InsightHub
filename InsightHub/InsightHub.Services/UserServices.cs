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
        private readonly InsightHubContext _context;

        public UserServices(InsightHubContext context)
        {
            _context = context;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Reports)
                .Include(u => u.Role)
                .Include(u => u.IndustrySubscriptions)
                .Include(u => u.Reports)
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new ArgumentNullException("User not found.");
            var userDTO = UserMapper.MapModelFromEntity(user);
            return userDTO;
        }

        public async Task<List<UserModel>> GetUsers(string search)
        {
            var users = await _context.Users
                .Where(u => !u.IsBanned && !u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            if(search != null)
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(search.ToLower())
                || u.LastName.ToLower().Contains(search.ToLower())
                || u.Email.ToLower().Contains(search.ToLower())).ToList();
            }

            return users;
        }

        public async Task<List<UserModel>> GetBannedUsers()
        {
            var users = await _context.Users
                .Where(u => u.IsBanned)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<List<UserModel>> GetPendingUsers()
        {
            var users = await _context.Users
                .Where(u => u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool isBanned, string banReason)
        {
            var user = await _context.Users
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsBanned)
                ?? throw new ArgumentNullException("User not found.");

            user.FirstName = firstName;
            user.LastName = lastName;
            user.LockoutEnabled = isBanned;
            user.BanReason = banReason;

            var userDTO = UserMapper.MapModelFromEntity(user);
            await _context.SaveChangesAsync();
            return userDTO;
        }

        public async Task BanUser(int id, string reason)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || user.LockoutEnabled)
                throw new ArgumentException("Unable to ban user.");

            user.IsBanned = true;
            user.BanReason = reason;
            user.LockoutEnd = DateTime.Parse("2555-01-01 00:00:00.00");
            await _context.SaveChangesAsync();
        }

        public async Task UnbanUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || !user.LockoutEnabled)
                throw new ArgumentException("Unable to unban user.");

            user.IsBanned = false;
            user.BanReason = string.Empty;
            user.LockoutEnd = DateTime.Parse("2000-01-01 00:00:00.00");
            _context.SaveChanges();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReportModel>> GetDownloadedReports(int userId)
        {
            var reports = await _context.DownloadedReports
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

        public async Task<List<ReportModel>> GetUploadedReports(int userId)
        {
            var reports = await _context.Reports
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

        public async Task<List<IndustryModel>> GetMySubscriptions(int userId)
        {
            var industries = await _context.IndustrySubscriptions
                .Include(ui => ui.Industry)
                .ThenInclude(i => i.Subscriptions)
                .Where(ui => ui.UserId == userId)
                .Select(ui => IndustryMapper.MapModelFromEntity(ui.Industry))
                .ToListAsync();

            return industries;
        }

    }
}
