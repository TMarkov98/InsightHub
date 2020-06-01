using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            var userDTO = UserMapper.MapModelFromEntity(user);
            return userDTO;
        }
        public async Task<List<UserModel>> GetUsers(string search)
        {
            var users = await _context.Users
                .Where(u => !u.IsBanned && !u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.Role)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            users = SearchUsers(search, users);

            return users;
        }
        public async Task<List<UserModel>> GetBannedUsers(string search)
        {
            var users = await _context.Users
                .Where(u => u.IsBanned)
                .Include(u => u.Role)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            users = SearchUsers(search, users);

            return users;
        }
        public async Task<List<UserModel>> GetPendingUsers(string search)
        {
            var users = await _context.Users
                .Where(u => u.IsPending)
                .Include(u => u.Role)
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            users = SearchUsers(search, users);

            return users;
        }

        public async Task<string> GetSubscribedUsers(string industry)
        {
            var users = await _context.IndustrySubscriptions
                .Where(u => u.Industry.Name == industry)
                .Include(u => u.User)
                .Include(u => u.Industry)
                .ToListAsync();
            List<string> sendTo = new List<string>();
            foreach (var user in users)
            {
                sendTo.Add(user.User.Email);
            }
            return string.Join(',', sendTo);
        }

        public async Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool isBanned, string banReason)
        {
            var user = await _context.Users
                .Include(u => u.Reports)
                .Include(u => u.IndustrySubscriptions)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsBanned);

            ValidateUserExists(user);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.IsBanned = isBanned;
            user.BanReason = banReason;

            var userDTO = UserMapper.MapModelFromEntity(user);
            user.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return userDTO;
        }
        public async Task BanUser(int id, string reason)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            if(user.IsBanned)
            {
                throw new ArgumentException("User already banned.");
            }

            user.IsBanned = true;
            user.BanReason = reason;
            await _context.SaveChangesAsync();
        }
        public async Task ApproveUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            user.IsPending = false;
            await _context.SaveChangesAsync();
        }
        public async Task UnbanUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            if(!user.IsBanned)
            {
                throw new ArgumentException("Unable to unban user. User not banned.");
            }
            user.IsBanned = false;
            user.BanReason = string.Empty;
            _context.SaveChanges();
        }
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ReportModel>> GetDownloadedReports(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ValidateUserExists(user);

            var reports = await _context.DownloadedReports
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Author)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Industry)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.ReportTags)
                .ThenInclude(r => r.Tag)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Downloads)
                .Where(ur => !ur.Report.IsDeleted)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ReportMapper.MapModelFromEntity(ur.Report))
                .ToListAsync();

            return reports;
        }
        public async Task<List<ReportModel>> GetUploadedReports(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ValidateUserExists(user);

            var reports = await _context.Reports
                .Include(r => r.Author)
                .Include(r => r.Industry)
                .Include(r => r.ReportTags)
                .ThenInclude(r => r.Tag)
                .Include(r => r.Downloads)
                .Where(r => r.AuthorId == userId)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            return reports;
        }
        public async Task<List<IndustryModel>> GetSubscriptions(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ValidateUserExists(user);

            var industries = await _context.IndustrySubscriptions
                .Include(ui => ui.Industry)
                .ThenInclude(i => i.SubscribedUsers)
                .Where(ui => ui.UserId == userId)
                .Select(ui => IndustryMapper.MapModelFromEntity(ui.Industry))
                .ToListAsync();

            return industries;
        }
        private List<UserModel> SearchUsers(string search, List<UserModel> users)
        {
            if(search != null)
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(search.ToLower())
                || u.LastName.ToLower().Contains(search.ToLower())
                || u.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            return users;
        }

        private void ValidateUserExists(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User not found.");
            }
        }

    }
}
