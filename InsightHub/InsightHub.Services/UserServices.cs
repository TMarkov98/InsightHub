using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class UserServices
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
                .Include(u => u.TagSubscriptions)
                .Include(u => u.IndustrySubscriptions)
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new ArgumentNullException("User not found.");
            var userDTO = UserMapper.MapModelFromEntity(user);
            return userDTO;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _insightHubContext.Users
                .Where(u => !u.LockoutEnabled && !u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.TagSubscriptions)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<List<UserModel>> GetBannedUsers()
        {
            var users = await _insightHubContext.Users
                .Where(u => !u.IsPending && u.LockoutEnabled)
                .Include(u => u.Reports)
                .Include(u => u.TagSubscriptions)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<List<UserModel>> GetPendingUsers()
        {
            var users = await _insightHubContext.Users
                .Where(u => !u.LockoutEnabled && u.IsPending)
                .Include(u => u.Reports)
                .Include(u => u.TagSubscriptions)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();
            return users;
        }

        public async Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool lockoutEnabled, string LockOutReason)
        {
            var user = await _insightHubContext.Users
                .Include(u => u.Reports)
                .Include(u => u.TagSubscriptions)
                .Include(u => u.IndustrySubscriptions)
                .FirstOrDefaultAsync(u => u.Id == id && !u.LockoutEnabled)
                ?? throw new ArgumentNullException("User not found.");

            user.FirstName = firstName;
            user.LastName = lastName;
            user.LockoutEnabled = lockoutEnabled;
            user.LockoutReason = LockOutReason;

            var userDTO = UserMapper.MapModelFromEntity(user);
            _insightHubContext.SaveChanges();
            return userDTO;
        }

        public async Task<bool> BanUser(int id, string reason)
        {
            var user = await _insightHubContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || user.LockoutEnabled)
                return false;

            user.LockoutEnabled = true;
            user.LockoutReason = reason;
            _insightHubContext.SaveChanges();
            return true;
        }

        public async Task<bool> UnBanUser(int id)
        {
            var user = await _insightHubContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null || !user.LockoutEnabled)
                return false;

            user.LockoutEnabled = false;
            user.LockoutReason = string.Empty;
            _insightHubContext.SaveChanges();
            return true;
        }
    }
}
